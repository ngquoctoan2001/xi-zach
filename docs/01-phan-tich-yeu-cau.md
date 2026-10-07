# Phân tích yêu cầu: game Xì Dách trên trình duyệt

Phiên bản 1.0 · 07/10/2026 · Trạng thái: **đã chốt**

Đây là bước 1 trong [quy trình phát triển](00-quy-trinh-phat-trien.md). Luật chơi chi tiết nằm ở [luat-xi-dach.md](luat-xi-dach.md); tài liệu này chỉ dẫn chiếu tới luật (ví dụ "luật 6.4"), không chép lại.

## 1. Giới thiệu

### 1.1 Mục đích

Liệt kê đầy đủ yêu cầu chức năng và phi chức năng của game, làm căn cứ cho thiết kế, lập trình và kiểm thử. Mỗi yêu cầu có một mã riêng để truy vết từ yêu cầu đến mã nguồn và ca kiểm thử.

### 1.2 Phạm vi sản phẩm

Một website chơi Xì Dách nhiều người theo thời gian thực bằng xu ảo, dùng nội bộ trong một nhóm quen biết, gồm khu vực người chơi và trang quản trị.

**Trong phạm vi:**

- Đăng nhập; tài khoản do quản trị viên cấp; phân quyền người chơi và quản trị viên.
- Ví xu ảo do quản trị viên nạp.
- Sảnh, phòng chơi và ván chơi theo đúng luật đã chốt.
- Lịch sử chơi và thống kê cá nhân.
- Trang quản trị.
- Đóng gói và triển khai bằng Docker.

**Ngoài phạm vi (phiên bản 1):**

- Tự đăng ký tài khoản, đăng nhập bằng Google, mọi chức năng dùng email (xác minh, quên mật khẩu).
- Nạp tiền thật, mua xu, đổi xu ra tiền hoặc vật phẩm, chuyển xu giữa người chơi.
- Thưởng xu hằng ngày, cứu trợ khi hết xu.
- Chat trong phòng, bảng xếp hạng.
- Ứng dụng di động cài đặt. Game chỉ chạy trên trình duyệt, kể cả trình duyệt điện thoại.
- Chơi với máy (bot), giải đấu, danh sách bạn bè, nhắn tin riêng.
- Ngôn ngữ khác ngoài tiếng Việt.

Danh sách chi tiết các chức năng đã loại nằm ở mục 3.11.

### 1.3 Mức ưu tiên

| Mức | Ý nghĩa |
|---|---|
| Must | bắt buộc có trong bản phát hành đầu tiên (MVP) |
| Should | nên có; làm ngay sau khi xong các mục Must |
| Could | có thì tốt; làm khi còn thời gian |
| Won't | không làm trong phiên bản 1 |

## 2. Tổng quan

### 2.1 Tác nhân

| Tác nhân | Mô tả |
|---|---|
| Khách | người chưa đăng nhập; chỉ thấy trang đăng nhập |
| Người chơi | tài khoản có vai trò người chơi |
| Quản trị viên (QTV) | tài khoản có vai trò quản trị, dùng trang quản trị, không ngồi chơi |
| Hệ thống | các xử lý tự động: đồng hồ đếm giờ, tự chơi khi hết giờ, đóng phòng trống, đối soát xu |

Trong một phòng, người chơi có thể giữ một hoặc nhiều vai trò sau:

| Vai trò trong phòng | Mô tả |
|---|---|
| Chủ phòng | người tạo phòng, hoặc người được chuyển quyền |
| Cái | người làm nhà cái |
| Con | người ngồi ghế và đặt cược |
| Người xem | ở trong phòng nhưng không ngồi ghế, hoặc đang chờ ván sau |

### 2.2 Ràng buộc

- **Công nghệ:** frontend Next.js, backend .NET, cơ sở dữ liệu PostgreSQL, đóng gói bằng Docker.
- **Nhân lực:** nhóm 2 người (xem quy trình phát triển).
- **Tiền tệ:** chỉ dùng xu ảo, vì đánh bài ăn tiền thật là vi phạm pháp luật Việt Nam.
- **Hình thức sử dụng:** theo Nghị định 147/2024/NĐ-CP (hiệu lực từ 25/12/2024), game phát hành tại Việt Nam không được dùng hình ảnh lá bài hay mô phỏng trò chơi có thưởng ở casino, nên game này không xin được giấy phép phát hành công khai. Vì vậy game chỉ dùng nội bộ: tài khoản do QTV cấp, không quảng bá, không kinh doanh. Muốn mở cho người ngoài nhóm thì phải hỏi ý kiến luật sư trước.
- **Ngôn ngữ:** giao diện tiếng Việt.

### 2.3 Giả định

- Người chơi dùng trình duyệt hiện đại, có mạng ổn định từ mức 4G trở lên.
- Nhóm người chơi nhỏ; cùng lúc có tối đa 5 phòng.
- Hệ thống chạy trên 1 máy chủ.

### 2.4 Vòng đời của phòng và ván

- **Phòng:** đang chờ → đang chơi ván → đang chờ → … → đóng.
- **Ván** (theo luật mục 6):
  1. Chờ bắt đầu.
  2. Đặt cược.
  3. Chia bài.
  4. Kiểm tra xì bàn, xì dách; ván có thể kết thúc ngay ở đây.
  5. Lượt con, lần lượt từng người.
  6. Lượt cái: rút bài và xét.
  7. Tính tiền.
  8. Hiện kết quả, chọn cái mới nếu cần, rồi quay lại bước 1.

Các trạng thái và điều kiện chuyển trạng thái sẽ được vẽ thành máy trạng thái ở bước thiết kế.

## 3. Yêu cầu chức năng

### 3.1 Trang công khai (PUB)

| Mã | Chức năng | Mô tả và tiêu chí chấp nhận | Tác nhân | Ưu tiên |
|---|---|---|---|---|
| PUB-01 | Trang đăng nhập | Trang đầu tiên của website là trang đăng nhập (AUTH-03); không có nút đăng ký. Người đã đăng nhập được đưa thẳng vào sảnh. | Khách | Must |
| PUB-02 | Hướng dẫn luật chơi | Trình bày luật cho người chơi, nội dung khớp với luat-xi-dach.md, có ví dụ tính điểm và tính tiền. | Người chơi | Should |
| PUB-03 | Điều khoản | Lần đăng nhập đầu tiên hiện điều khoản ngắn: xu ảo không có giá trị quy đổi, người chơi phải từ 18 tuổi. Phải bấm đồng ý mới vào được sảnh. | Người chơi | Should |

### 3.2 Tài khoản và xác thực (AUTH)

| Mã | Chức năng | Mô tả và tiêu chí chấp nhận | Tác nhân | Ưu tiên |
|---|---|---|---|---|
| AUTH-03 | Đăng nhập | Đăng nhập bằng tên đăng nhập và mật khẩu do QTV cấp. Chọn "Ghi nhớ đăng nhập" thì phiên giữ 30 ngày; không chọn thì phiên hết khi đóng trình duyệt. Sai mật khẩu 5 lần liên tiếp thì khóa đăng nhập tài khoản đó 15 phút (QTV mở khóa sớm được). Thông báo lỗi không tiết lộ tài khoản có tồn tại hay không. | Khách | Must |
| AUTH-04 | Đăng xuất | Hủy phiên hiện tại. Nếu đang ở trong ván, phần chơi còn lại do game tự chơi (luật 5.5). | Người chơi, QTV | Must |
| AUTH-06 | Đổi mật khẩu | Nhập mật khẩu cũ và mật khẩu mới. Mật khẩu dài từ 8 ký tự, có cả chữ và số. Đổi xong thì các phiên khác bị đăng xuất. | Người chơi, QTV | Must |
| AUTH-07 | Phân quyền | Hai vai trò: người chơi và QTV. Trang quản trị và API quản trị chỉ QTV dùng được; máy chủ kiểm tra quyền ở mọi yêu cầu. Tài khoản QTV không ngồi ghế chơi. | Hệ thống | Must |
| AUTH-08 | Tài khoản bị khóa | Không đăng nhập được, thấy lý do và thời hạn khóa. Nếu đang online thì bị đăng xuất ngay; phần ván đang chơi do game tự chơi. | Người chơi | Must |
| AUTH-09 | Mỗi tài khoản một chỗ | Một tài khoản chỉ ở trong 1 phòng tại một thời điểm. Mở game ở tab hoặc thiết bị khác thì phiên mới tiếp quản, phiên cũ bị ngắt kết nối khỏi phòng. | Hệ thống | Must |
| AUTH-12 | Bắt buộc đổi mật khẩu | Tài khoản mới tạo (ADM-15, SYS-04) hoặc vừa được QTV đặt lại mật khẩu (ADM-07) phải đổi mật khẩu ngay sau khi đăng nhập; chưa đổi thì không vào được sảnh hay trang quản trị. | Người chơi, QTV | Must |

### 3.3 Hồ sơ người chơi (PROF)

| Mã | Chức năng | Mô tả và tiêu chí chấp nhận | Tác nhân | Ưu tiên |
|---|---|---|---|---|
| PROF-01 | Xem hồ sơ của mình | Tên hiển thị, ảnh đại diện, số xu, ngày tham gia, thống kê (HIS-03). | Người chơi | Must |
| PROF-02 | Sửa hồ sơ | Đổi tên hiển thị (2–20 ký tự, không trùng với người khác, không chứa từ ngữ cấm) và chọn ảnh đại diện từ bộ ảnh có sẵn. | Người chơi | Should |
| PROF-03 | Xem hồ sơ người khác | Bấm vào ảnh đại diện trong phòng để xem tên, ảnh, số ván đã chơi và tỉ lệ thắng. | Người chơi | Could |
| PROF-04 | Cài đặt | Bật/tắt âm thanh; bật/tắt tùy chọn "Không nhận làm cái" (luật 5.3). Lưu theo tài khoản. | Người chơi | Should |

### 3.4 Ví xu (WAL)

| Mã | Chức năng | Mô tả và tiêu chí chấp nhận | Tác nhân | Ưu tiên |
|---|---|---|---|---|
| WAL-01 | Số dư và lịch sử giao dịch | Xem số dư và lịch sử giao dịch. Mỗi dòng có: thời gian, loại (xu ban đầu, QTV nạp, QTV trừ, thắng ván, thua ván), số xu cộng hoặc trừ, số dư sau giao dịch, mã ván nếu có. Lọc theo loại và khoảng thời gian; mỗi trang 20 dòng. | Người chơi | Must |
| WAL-02 | Xu ban đầu | Khi tạo tài khoản người chơi, QTV nhập số xu ban đầu (mặc định 10.000). Các tài khoản tạo sẵn (SYS-04) có 10.000 xu. | QTV, hệ thống | Must |

### 3.5 Sảnh (LOB)

| Mã | Chức năng | Mô tả và tiêu chí chấp nhận | Tác nhân | Ưu tiên |
|---|---|---|---|---|
| LOB-01 | Danh sách phòng | Mỗi phòng hiện: tên, mã phòng, chế độ làm cái, kiểu cược và mức cược, số ghế đã có người trên 8, trạng thái (đang chờ, đang chơi), có khóa mật khẩu hay không. Danh sách tự cập nhật khi có thay đổi, không cần tải lại trang. | Người chơi | Must |
| LOB-02 | Lọc và sắp xếp | Lọc theo mức cược, còn ghế trống, chế độ làm cái; có tùy chọn ẩn những phòng không đủ xu để vào. | Người chơi | Should |
| LOB-03 | Tạo phòng | Nhập tên phòng (tối đa 30 ký tự). Chọn chế độ làm cái: cố định, hoặc xoay tua kèm số ván mỗi lượt từ 1 đến 10. Chọn kiểu cược: cố định kèm mức cược, hoặc tự do kèm mức tối thiểu và tối đa. Đặt mật khẩu phòng nếu muốn. Mức cược phải nằm trong khoảng hệ thống cho phép (mặc định 100–10.000 xu, ADM-10); mức tối đa không nhỏ hơn mức tối thiểu. Hệ thống có tối đa 5 phòng mở cùng lúc (ADM-10); đã đủ thì không tạo thêm được và được gợi ý vào phòng có sẵn. Người tạo trở thành chủ phòng và vào phòng ngay. | Người chơi | Must |
| LOB-04 | Vào phòng | Vào bằng cách chọn trong danh sách, nhập mã phòng hoặc mở liên kết mời. Vào với tư cách người xem, rồi chọn ghế trống để ngồi (ROOM-01). Phòng có mật khẩu thì phải nhập đúng. Phòng đã đủ 20 người xem thì báo đầy. | Người chơi | Must; mật khẩu và liên kết mời: Should |
| LOB-05 | Chơi nhanh | Tự đưa vào một phòng còn ghế có mức cược phù hợp số dư; không có thì tạo phòng mới với cấu hình mặc định. | Người chơi | Could |

### 3.6 Phòng chơi (ROOM)

| Mã | Chức năng | Mô tả và tiêu chí chấp nhận | Tác nhân | Ưu tiên |
|---|---|---|---|---|
| ROOM-01 | Ngồi ghế | Bàn có 8 ghế: 1 người làm cái, tối đa 7 người làm con (luật 5.1). Người ngồi vào giữa ván phải chờ ván sau. Không đủ xu tối thiểu thì không ngồi được và được báo số xu cần có. | Người chơi | Must |
| ROOM-02 | Đứng dậy | Rời ghế để làm người xem. Nếu đang trong ván thì rời sau khi ván kết thúc; phần chơi còn lại do game tự chơi. Nếu là cái thì chọn cái mới theo luật 5.3. | Người chơi | Must |
| ROOM-03 | Rời phòng | Quay về sảnh. Nếu đang trong ván thì xử lý như ROOM-02. | Người chơi | Must |
| ROOM-04 | Hiển thị bàn chơi | Hiển thị: ghế, tên, ảnh, số xu của từng người; ai là cái, ai là chủ phòng; ai đang tới lượt và đồng hồ đếm ngược; số lá mỗi người đang cầm (úp); bài của chính mình kèm tổng nút (tính A theo luật mục 3); các lá đã lật; kết quả và số xu của từng con ngay khi được chốt. | Người chơi | Must |
| ROOM-05 | Người xem | Thấy mọi thông tin công khai như người ngồi, nhưng không thấy bài úp của ai. Tối đa 20 người xem mỗi phòng. | Người chơi | Must |
| ROOM-06 | Nhận hoặc từ chối làm cái | Hộp hỏi nhận làm cái, đếm ngược 10 giây. Các trường hợp từ chối, không ai nhận và cái xin nghỉ xử lý theo luật 5.3. | Người chơi | Must |
| ROOM-07 | Chủ phòng đổi cái | Ở chế độ cái cố định, chủ phòng chọn người làm cái mới; chỉ áp dụng giữa hai ván và người được chọn phải đồng ý (luật 5.2, 5.3). | Chủ phòng | Must |
| ROOM-08 | Chuyển quyền chủ phòng | Chủ phòng rời phòng thì quyền chủ phòng tự chuyển cho người vào phòng sớm nhất (luật 5.5). Chủ phòng cũng có thể tự chuyển quyền cho người khác. | Hệ thống, chủ phòng | Must; tự chuyển: Should |
| ROOM-09 | Mời ra khỏi phòng | Chủ phòng mời một người ra khỏi phòng khi người đó không ở trong ván đang chơi. Người bị mời ra không vào lại phòng đó trong 10 phút. | Chủ phòng | Could |
| ROOM-11 | Âm thanh và hiệu ứng | Âm thanh và hoạt ảnh khi chia, rút, lật bài và khi thắng thua; tắt được trong cài đặt (PROF-04). | Người chơi | Should |
| ROOM-12 | Tự đóng phòng | Phòng không còn ai ngồi ghế trong 5 phút thì tự đóng; người xem được đưa về sảnh. | Hệ thống | Must |

### 3.7 Ván chơi (GAME)

| Mã | Chức năng | Mô tả và tiêu chí chấp nhận | Tác nhân | Ưu tiên |
|---|---|---|---|---|
| GAME-01 | Bắt đầu ván | Cái bấm "Bắt đầu" khi có ít nhất 1 con đủ điều kiện; nếu không bấm thì ván tự bắt đầu sau 15 giây (luật 6.1). | Cái, hệ thống | Must |
| GAME-02 | Đặt cược | Bàn cố định: game tự đặt cược. Bàn tự do: chọn số xu trong khoảng tối thiểu–tối đa trong 10 giây; hết giờ thì tự đặt theo luật 5.4. Không đặt quá số dư. | Con | Must |
| GAME-03 | Xào và chia bài | Máy chủ xào bài (FAIR-03) và chia đúng thứ tự ở luật 6.2. Mỗi người chỉ nhận thông tin lá bài của chính mình. | Hệ thống | Must |
| GAME-04 | Xì bàn, xì dách | Kiểm tra và xử lý theo luật 6.3, kể cả kết thúc ván sớm khi cái có xì bàn hoặc xì dách. | Hệ thống | Must |
| GAME-05 | Lượt con | Theo luật 6.4: nút Rút và Dằn chỉ hiện khi tới lượt; lá rút lấy từ đáy bài tì; dằn dưới 16 nút phải xác nhận; lượt tự kết thúc khi quắc, đền hoặc đủ 5 lá; mỗi quyết định có 15 giây, hết giờ thì game tự xử lý. | Con | Must |
| GAME-06 | Lượt cái | Theo luật 6.5: cái rút bài; khi có từ 15 nút thì chọn con để xét; xét xong được rút tiếp; bấm dằn hẳn để lật hết các con còn lại. Kết quả từng con được chốt ngay khi xét và hiện cho cả bàn. Hết giờ thì game tự xử lý. | Cái | Must |
| GAME-07 | Tính tiền | Theo luật mục 7 và 8: so bài, hệ số, trả thay cái, giới hạn 12 lần, đền làng, không đủ xu. Số dư của mọi người được cập nhật một lần khi ván kết thúc và ghi vào lịch sử giao dịch. | Hệ thống | Must |
| GAME-08 | Kết quả ván | Lật bài của mọi người. Bảng kết quả ghi cho từng người: bài, loại bài, thắng/thua/hòa/bị phạt và số xu cộng hoặc trừ. Bảng hiện 5 giây rồi chuyển sang chờ ván mới. | Hệ thống | Must |
| GAME-09 | Rớt mạng và kết nối lại | Mất kết nối thì game tự chơi thay theo luật hết giờ. Kết nối lại khi phòng còn mở thì thấy đúng trạng thái hiện tại và điều khiển tiếp được. | Người chơi | Must |
| GAME-10 | Đổi cái sau ván | Chế độ xoay tua: đếm số ván của lượt làm cái, hết lượt thì chọn cái mới (luật 5.2, 5.3). Chế độ cố định: đổi khi chủ phòng chọn, cái xin nghỉ, cái rời phòng hoặc cái không còn đủ xu. | Hệ thống | Must |
| GAME-11 | Hủy ván bất thường | Ván bị gián đoạn vì lỗi máy chủ hoặc vì QTV đóng phòng thì bị hủy: không ai được hay mất xu của ván đó (luật 6.7). | Hệ thống | Must |
| GAME-12 | Nhật ký ván | Mỗi ván lưu: mã ván, phòng, người tham gia và vai trò, phần cược, thứ tự đầy đủ của bộ bài sau khi xào, từng hành động kèm thời điểm, kết quả và tiền. Không sửa, không xóa được. | Hệ thống | Must |

### 3.8 Lịch sử và thống kê (HIS)

| Mã | Chức năng | Mô tả và tiêu chí chấp nhận | Tác nhân | Ưu tiên |
|---|---|---|---|---|
| HIS-01 | Lịch sử ván | Danh sách các ván đã chơi: thời gian, phòng, vai trò, bài, loại bài, kết quả, số xu cộng hoặc trừ; mỗi trang 20 dòng. | Người chơi | Should |
| HIS-02 | Xem lại ván | Xem lại từng bước một ván mình đã chơi; chỉ hiện các lá đã được lật trong ván. | Người chơi | Could |
| HIS-03 | Thống kê cá nhân | Số ván; số ván thắng, thua, hòa; số lần xì bàn, xì dách, ngũ linh, quắc, non, đền; số ván làm cái; tổng lãi hoặc lỗ. | Người chơi | Should |

### 3.9 Quản trị (ADM)

| Mã | Chức năng | Mô tả và tiêu chí chấp nhận | Tác nhân | Ưu tiên |
|---|---|---|---|---|
| ADM-01 | Vào trang quản trị | Đường dẫn riêng, chỉ QTV vào được, dùng chung hệ thống đăng nhập. | QTV | Must |
| ADM-02 | Bảng tổng quan | Số người đang online, số phòng đang mở và đang chơi, số ván trong ngày, tổng xu đang lưu hành. | QTV | Should |
| ADM-03 | Quản lý người dùng | Tìm theo tên đăng nhập hoặc tên hiển thị. Xem chi tiết: thông tin, vai trò, trạng thái, số dư, lịch sử giao dịch, lịch sử ván, lần đăng nhập gần nhất. | QTV | Must |
| ADM-04 | Khóa và mở khóa tài khoản | Khóa có thời hạn hoặc vĩnh viễn, bắt buộc nhập lý do; có hiệu lực ngay (AUTH-08). Mở khóa được cả trường hợp bị khóa do nhập sai mật khẩu (AUTH-03). | QTV | Must |
| ADM-05 | Cấp và thu quyền QTV | Không được tự thu quyền của chính mình; hệ thống luôn còn ít nhất 1 QTV. | QTV | Should |
| ADM-06 | Nạp và trừ xu | Nạp hoặc trừ xu cho người chơi, bắt buộc nhập lý do. Nạp không giới hạn và không trừ vào tài khoản QTV (tài khoản QTV không có ví xu). Trừ không được làm số dư âm. Nếu người đó đang trong ván thì áp dụng sau khi ván kết thúc. Ghi vào lịch sử giao dịch với loại "QTV nạp" hoặc "QTV trừ". | QTV | Must |
| ADM-07 | Đặt lại mật khẩu | Đặt mật khẩu tạm cho một tài khoản: QTV tự nhập, hoặc để hệ thống tạo ngẫu nhiên. Mật khẩu tạm hiện đúng một lần để QTV gửi cho người dùng. Mọi phiên của người đó bị đăng xuất; lần đăng nhập kế tiếp bắt buộc đổi mật khẩu (AUTH-12). | QTV | Must |
| ADM-08 | Quản lý phòng | Xem danh sách phòng đang mở và trạng thái công khai của bàn. Không xem được bài úp khi ván đang diễn ra. Đóng được phòng; ván dở bị hủy theo GAME-11. | QTV | Must |
| ADM-09 | Tra cứu nhật ký ván | Tìm theo mã ván, người chơi hoặc khoảng thời gian. Xem toàn bộ diễn biến, kể cả thứ tự bộ bài, sau khi ván đã kết thúc. | QTV | Must |
| ADM-10 | Cấu hình hệ thống | Sửa các thông số ở luật mục 9, khoảng mức cược được phép khi tạo phòng (mặc định 100–10.000 xu), số phòng tối đa (mặc định 5) và xu ban đầu mặc định (10.000). Thay đổi chỉ áp dụng cho phòng tạo sau đó; có lịch sử thay đổi. | QTV | Should |
| ADM-11 | Nhật ký quản trị | Ghi lại mọi thao tác của QTV: ai, lúc nào, làm gì, với đối tượng nào, giá trị trước và sau. Không sửa, không xóa được; xem và lọc được. | QTV, hệ thống | Must |
| ADM-12 | Thông báo hệ thống | Tạo thông báo hiện ở sảnh trong khoảng thời gian chọn trước. | QTV | Could |
| ADM-13 | Chế độ bảo trì | Khi bật thì không tạo được phòng mới và không bắt đầu ván mới; các ván đang chơi vẫn được chơi hết. | QTV | Could |
| ADM-15 | Tạo tài khoản | Nhập tên đăng nhập (3–20 ký tự, chỉ gồm chữ không dấu, số và dấu gạch dưới, không trùng, không phân biệt hoa thường), tên hiển thị, vai trò (người chơi hoặc QTV) và mật khẩu tạm (tự nhập hoặc để hệ thống tạo ngẫu nhiên, hiện đúng một lần). Với người chơi thì nhập thêm số xu ban đầu (WAL-02). Người được tạo phải đổi mật khẩu ở lần đăng nhập đầu (AUTH-12). | QTV | Must |
| ADM-16 | Xóa tài khoản | Xóa mềm: tài khoản không đăng nhập được nữa; tên đăng nhập và tên hiển thị được thay bằng mã ẩn danh; lịch sử ván giữ lại ở dạng ẩn danh. Không xóa được khi người đó đang trong ván, và không xóa được QTV cuối cùng. | QTV | Should |

### 3.10 Xử lý tự động (SYS)

| Mã | Chức năng | Mô tả và tiêu chí chấp nhận | Tác nhân | Ưu tiên |
|---|---|---|---|---|
| SYS-01 | Đồng hồ và tự xử lý | Đếm giờ cho: nhận làm cái (10 giây), đặt cược (10 giây), mỗi quyết định rút, dằn hoặc xét (15 giây), tự bắt đầu ván (15 giây). Hết giờ thì xử lý đúng theo luật. | Hệ thống | Must |
| SYS-03 | Đối soát xu | Mỗi ngày tự kiểm tra: số dư của mọi tài khoản khớp với tổng lịch sử giao dịch, và tổng tiền của mỗi ván bằng 0. Có sai lệch thì báo cho QTV. | Hệ thống | Must |
| SYS-04 | Dữ liệu khởi tạo | Lần chạy đầu tiên, hệ thống tự tạo 1 tài khoản QTV và 5 tài khoản người chơi; mỗi tài khoản người chơi có 10.000 xu, ghi giao dịch loại "xu ban đầu". Tên đăng nhập và mật khẩu lấy từ cấu hình môi trường, không ghi cứng trong mã nguồn. Cả 6 tài khoản phải đổi mật khẩu ở lần đăng nhập đầu (AUTH-12). Chạy lại không tạo trùng. | Hệ thống | Must |

### 3.11 Các chức năng đã loại (Won't)

Giữ lại để truy vết; các mã này không được dùng lại cho chức năng khác.

| Mã | Chức năng | Lý do loại |
|---|---|---|
| AUTH-01 | Tự đăng ký | Tài khoản do QTV tạo (ADM-15). |
| AUTH-02 | Xác minh email | Hệ thống không dùng email. |
| AUTH-05 | Quên mật khẩu qua email | QTV đặt lại mật khẩu (ADM-07). |
| AUTH-10 | Đăng nhập bằng Google | Không cần. |
| AUTH-11 | Đăng ký bằng mã mời | Không còn tự đăng ký. |
| PROF-05 | Người chơi tự xóa tài khoản | QTV xóa (ADM-16). |
| WAL-03 | Thưởng hằng ngày | Xu do QTV nạp. |
| WAL-04 | Cứu trợ khi hết xu | Xu do QTV nạp. |
| ROOM-10 | Chat nhanh | Không có chat trong phòng. |
| HIS-04 | Bảng xếp hạng | Không cần. |
| ADM-14 | Quản lý mã mời | Không còn tự đăng ký. |
| SYS-02 | Đổi ngày cho thưởng hằng ngày | Không còn thưởng hằng ngày. |

## 4. Yêu cầu phi chức năng

### 4.1 Hiệu năng và sức chứa (PERF)

| Mã | Yêu cầu | Cách kiểm tra |
|---|---|---|
| PERF-01 | Từ lúc người chơi bấm một hành động trong ván đến lúc mọi người trong phòng thấy cập nhật: không quá 300 ms ở phân vị 95, khi độ trễ mạng không quá 100 ms. Thời gian xử lý trên máy chủ không quá 50 ms ở phân vị 95. | Kiểm thử tải |
| PERF-02 | Các API thông thường (đăng nhập, sảnh, lịch sử, quản trị) phản hồi trong 500 ms ở phân vị 95. | Kiểm thử tải |
| PERF-03 | Trang sảnh và trang bàn chơi có LCP không quá 2,5 giây trên mạng 4G. | Lighthouse, chế độ di động |
| PERF-04 | 1 máy chủ 2 vCPU, 4 GB RAM chịu được 5 phòng chơi cùng lúc (tối đa 40 người ngồi chơi và 100 người xem) mà vẫn đạt PERF-01. Kiểm thử tải ở mức gấp đôi (10 phòng) để có dư địa. | Kiểm thử tải |

### 4.2 Toàn vẹn dữ liệu và độ tin cậy (REL)

| Mã | Yêu cầu | Cách kiểm tra |
|---|---|---|
| REL-01 | Xu chỉ được sinh ra khi QTV nạp (kể cả xu ban đầu) và chỉ mất đi khi QTV trừ. Tổng tiền của mỗi ván luôn bằng 0. | Kiểm thử tự động và đối soát SYS-03 |
| REL-02 | Kết quả ván, số dư và lịch sử giao dịch được ghi trong cùng một giao dịch cơ sở dữ liệu: hoặc ghi đủ hết, hoặc không ghi gì. | Kiểm thử tích hợp có giả lập lỗi giữa chừng |
| REL-03 | Mỗi hành động (rút, dằn, xét, đặt cược) chỉ được xử lý một lần, kể cả khi người chơi bấm nhiều lần hoặc mạng gửi lại. Hành động sai lượt hoặc không hợp lệ bị từ chối. | Kiểm thử tự động |
| REL-04 | Sau khi máy chủ khởi động lại, không tài khoản nào bị sai số dư; ván dở được hủy theo GAME-11; người chơi kết nối lại thì thấy thông báo. | Kiểm thử thủ công có kịch bản |
| REL-05 | Hệ thống hoạt động ít nhất 99% thời gian mỗi tháng, không tính các đợt bảo trì có báo trước. | Giám sát uptime |
| REL-06 | Sao lưu cơ sở dữ liệu tự động mỗi ngày, giữ 7 bản gần nhất. Mất dữ liệu tối đa 24 giờ (RPO); khôi phục xong trong 1 giờ (RTO). Đã diễn tập khôi phục ít nhất một lần trước khi ra mắt. | Diễn tập khôi phục |

### 4.3 Bảo mật (SEC)

| Mã | Yêu cầu | Cách kiểm tra |
|---|---|---|
| SEC-01 | Mật khẩu được băm bằng thuật toán chuyên dùng cho mật khẩu (PBKDF2 của ASP.NET Core Identity hoặc Argon2id). Không lưu và không ghi log mật khẩu thô hay token. Mật khẩu tạm cũng chỉ lưu dạng băm. | Xem lại mã nguồn, kiểm tra log |
| SEC-02 | Mọi kết nối đi qua HTTPS và WSS. Cookie phiên có HttpOnly, Secure, SameSite. | Kiểm tra cấu hình |
| SEC-03 | Giới hạn tần suất: đăng nhập tối đa 10 lần mỗi phút mỗi IP; hành động trong ván tối đa 10 lần mỗi giây mỗi người. Vượt giới hạn thì bị từ chối và ghi log. | Kiểm thử tự động |
| SEC-04 | Mọi API và mọi thông điệp thời gian thực đều được máy chủ kiểm tra đăng nhập, vai trò và quyền với phòng, ván (ví dụ: chỉ cái mới được xét). | Kiểm thử tự động |
| SEC-05 | Đáp ứng OWASP Top 10: kiểm tra đầu vào ở máy chủ, truy vấn tham số hóa, chống XSS (escape đầu ra, Content-Security-Policy), chống CSRF, không để lộ chi tiết lỗi ra ngoài. | Quét bảo mật, xem lại mã nguồn |
| SEC-06 | Chuỗi kết nối, khóa ký token và mật khẩu của các tài khoản tạo sẵn lấy từ biến môi trường hoặc kho bí mật; không có trong mã nguồn và git. | Quét bí mật trong CI |
| SEC-07 | CI quét lỗ hổng của thư viện; không ra mắt khi còn lỗ hổng từ mức High trở lên. | CI |

### 4.4 Công bằng và chống gian lận (FAIR)

| Mã | Yêu cầu | Cách kiểm tra | Ưu tiên |
|---|---|---|---|
| FAIR-01 | Trình duyệt chỉ gửi ý định (rút, dằn, xét, đặt cược). Điểm, kết quả và tiền đều do máy chủ tính. | Xem lại thiết kế, kiểm thử | Must |
| FAIR-02 | Trình duyệt chỉ nhận những lá bài người đó được phép thấy. Thứ tự bộ bài và bài úp của người khác không bao giờ được gửi xuống trước khi lật. | Xem toàn bộ dữ liệu mạng của một người chơi trong cả ván | Must |
| FAIR-03 | Xào bài bằng bộ sinh số ngẫu nhiên mật mã (CSPRNG) theo thuật toán Fisher–Yates. | Thử 1 triệu lần xào, kiểm định chi-bình phương | Must |
| FAIR-04 | Công bố mã băm của thứ tự bộ bài trước khi chia và công bố thứ tự bộ bài sau ván, để người chơi tự kiểm tra ván không bị sắp đặt. | Kiểm thử tự động | Could |
| FAIR-05 | Ghi nhận IP và báo cho QTV khi nhiều tài khoản từ cùng một IP ngồi chung bàn. | Kiểm thử tự động | Could |

### 4.5 Khả năng sử dụng (USA)

| Mã | Yêu cầu | Cách kiểm tra |
|---|---|---|
| USA-01 | Toàn bộ giao diện là tiếng Việt có dấu; thuật ngữ thống nhất với luật (cái, con, rút, dằn, xét, non, quắc, đền…). | Xem lại giao diện |
| USA-02 | Luôn hiển thị tổng nút hiện tại của mình, ai đang tới lượt, đồng hồ đếm ngược và số xu của mình. Kết quả từng con hiện ngay khi được chốt. | Kiểm thử đầu-cuối |
| USA-03 | Nút hành động chỉ hiện khi hành động đó hợp lệ. Dằn non phải xác nhận. Thông báo lỗi bằng lời dễ hiểu, không hiện mã lỗi kỹ thuật. | Kiểm thử đầu-cuối |
| USA-04 | Mỗi màn hình có trạng thái đang tải, trạng thái trống (ví dụ sảnh chưa có phòng) và trạng thái lỗi kèm nút thử lại. Mất kết nối thời gian thực thì hiện thông báo và tự kết nối lại. | Kiểm thử đầu-cuối |
| USA-05 | Chữ có độ tương phản từ 4,5:1; các thao tác chính dùng được bằng bàn phím trên máy tính; vùng bấm trên điện thoại từ 44×44 px. | Lighthouse, kiểm tra thủ công |
| USA-06 | Thời gian lưu theo UTC, hiển thị theo giờ Việt Nam (UTC+7). | Kiểm thử tự động |

### 4.6 Tương thích (COMP)

| Mã | Yêu cầu | Cách kiểm tra |
|---|---|---|
| COMP-01 | Chạy được trên 2 phiên bản mới nhất của Chrome, Edge, Firefox và Safari trên máy tính; Chrome trên Android và Safari trên iOS. | Kiểm thử đầu-cuối trên nhiều trình duyệt |
| COMP-02 | Trên điện thoại, bàn chơi chỉ chơi ở màn hình ngang; khi cầm dọc thì hiện lời nhắc xoay ngang. Các trang khác (đăng nhập, sảnh, hồ sơ) dùng được cả dọc lẫn ngang. Trang quản trị ưu tiên máy tính. Chiều rộng tối thiểu 360 px. | Kiểm thử thủ công |

### 4.7 Bảo trì và kiểm thử (MAIN)

| Mã | Yêu cầu | Cách kiểm tra |
|---|---|---|
| MAIN-01 | Bộ luật chơi (tính điểm, so bài, tính tiền) là một thư viện riêng, không phụ thuộc web, cơ sở dữ liệu hay kết nối thời gian thực. | Xem lại kiến trúc |
| MAIN-02 | Thư viện luật chơi có độ bao phủ nhánh từ 95%; mọi ví dụ trong luat-xi-dach.md đều là một ca kiểm thử tự động. | Báo cáo coverage |
| MAIN-03 | Có kiểm thử tích hợp cho API và cơ sở dữ liệu; có kiểm thử đầu-cuối cho luồng: QTV tạo tài khoản → người chơi đăng nhập, đổi mật khẩu → tạo phòng → chơi hết 1 ván → xem lịch sử. | CI |
| MAIN-04 | Mỗi lần đẩy mã, CI chạy build, lint và toàn bộ kiểm thử; lỗi thì không gộp được. | CI |
| MAIN-05 | Mọi thay đổi cấu trúc cơ sở dữ liệu đi qua migration có phiên bản, chạy tự động khi triển khai. | Xem lại quy trình triển khai |
| MAIN-06 | Mỗi bước trong quy trình có tài liệu trong thư mục docs; mã yêu cầu được nhắc trong thiết kế và trong tên ca kiểm thử. | Xem lại tài liệu |

### 4.8 Vận hành (OPS)

| Mã | Yêu cầu | Cách kiểm tra |
|---|---|---|
| OPS-01 | Log có cấu trúc (JSON), có mã tương quan cho mỗi yêu cầu và mã ván cho mọi log trong ván; giữ log 14 ngày. | Kiểm tra log |
| OPS-02 | Có endpoint kiểm tra sức khỏe cho backend và cơ sở dữ liệu; container tự khởi động lại khi lỗi. | Kiểm tra thủ công |
| OPS-03 | Đo và xem được: số người online, số phòng, số ván mỗi phút, độ trễ hành động (PERF-01), số lỗi. | Dashboard giám sát |
| OPS-04 | Toàn hệ thống (frontend, backend, PostgreSQL, reverse proxy) chạy bằng Docker Compose với 1 lệnh; cấu hình tách riêng cho môi trường dev, staging và production. | Chạy thử trên máy mới |
| OPS-05 | Mỗi lần cập nhật phiên bản gián đoạn tối đa 5 phút; có báo trước trên sảnh (ADM-12) và dùng chế độ bảo trì (ADM-13) để không cắt ngang ván. | Diễn tập cập nhật |

### 4.9 Pháp lý và dữ liệu cá nhân (LEGAL)

| Mã | Yêu cầu | Cách kiểm tra |
|---|---|---|
| LEGAL-01 | "Nạp xu" chỉ có nghĩa là QTV cấp xu ảo; xu không được đổi lấy tiền thật hay vật phẩm theo bất kỳ chiều nào, và không chuyển được giữa người chơi. Điều khoản (PUB-03) ghi rõ xu không có giá trị quy đổi. | Xem lại chức năng và điều khoản |
| LEGAL-02 | Chỉ lưu tên đăng nhập, tên hiển thị và dữ liệu chơi; không thu thập email hay giấy tờ tùy thân. QTV xóa tài khoản theo yêu cầu của người chơi (ADM-16). Tuân thủ Luật Bảo vệ dữ liệu cá nhân 2025 (số 91/2025/QH15, hiệu lực từ 01/01/2026). | Xem lại dữ liệu lưu trữ |
| LEGAL-03 | Chỉ dùng nội bộ theo ràng buộc ở mục 2.2: tài khoản do QTV cấp, không quảng bá, không kinh doanh. | Xem lại trước khi triển khai |

## 5. Các quyết định đã chốt

| Mã | Câu hỏi | Quyết định |
|---|---|---|
| R0 | Hình thức sử dụng | Dùng nội bộ trong nhóm quen biết: tài khoản do QTV cấp, không tự đăng ký, không quảng bá, không kinh doanh. |
| R1 | Xác minh email | Hệ thống không dùng email. |
| R2 | Đăng nhập bằng Google | Không làm. |
| R3 | Nguồn xu | QTV nạp; tài khoản tạo sẵn có 10.000 xu; không có thưởng hằng ngày hay cứu trợ. Mức cược khi tạo phòng từ 100 đến 10.000 xu. |
| R4 | Chat trong phòng | Không có. |
| R5 | Mời người ra khỏi phòng | Có, mức Could. |
| R6 | Phòng khóa bằng mật khẩu | Có, mức Should. |
| R7 | Ván bị gián đoạn bất thường | Hủy ván, không tính tiền; đã thêm vào luật 6.7. |
| R8 | Số người xem mỗi phòng | Tối đa 20. |
| R9 | QTV xem bài úp khi ván đang diễn ra | Không; chỉ xem qua nhật ký sau khi ván kết thúc. |
| R10 | Tài khoản QTV ngồi chơi | Không; muốn chơi thì dùng một tài khoản người chơi riêng. |
| R11 | Xóa tài khoản | QTV xóa mềm và ẩn danh (ADM-16). |
| R12 | Bảng xếp hạng | Không làm. |
| R13 | Độ tuổi | Điều khoản ở lần đăng nhập đầu yêu cầu người chơi từ 18 tuổi. |
| R14 | Sức chứa | 5 phòng chơi cùng lúc. |
| R15 | Điện thoại | Bàn chơi chỉ chơi ở màn hình ngang. |
| R16 | Phạm vi MVP | Mọi mục Must; các mục Should làm ngay sau MVP. |

| X1 | Cách đăng nhập | Chỉ đăng nhập bằng tên đăng nhập và mật khẩu do QTV cấp. |
| X2 | Tài khoản QTV | QTV nạp xu không giới hạn và không bị trừ vào tài khoản của mình; hệ thống có thể có nhiều QTV (ADM-05). |

## 6. Bước tiếp theo

Sau khi tài liệu này được chốt, chuyển sang bước 2: thiết kế hệ thống (kiến trúc, cơ sở dữ liệu, API và kết nối thời gian thực, máy trạng thái của ván).

## 7. Lịch sử thay đổi

- **1.0 (07/10/2026):** chốt bước 1; xác nhận X1, X2.
- **0.2 (07/10/2026):**
  - Bỏ tự đăng ký và mọi chức năng dùng email: QTV tạo tài khoản (ADM-15) và đặt lại mật khẩu (ADM-07, nâng lên Must); thêm bắt buộc đổi mật khẩu lần đầu (AUTH-12).
  - Xu do QTV nạp (ADM-06); bỏ thưởng hằng ngày và cứu trợ.
  - Thêm dữ liệu khởi tạo: 1 QTV và 5 người chơi, mỗi người chơi 10.000 xu (SYS-04). Thêm xóa tài khoản do QTV làm (ADM-16).
  - Bỏ chat trong phòng và bảng xếp hạng. Sức chứa 5 phòng, giới hạn 5 phòng mở cùng lúc. Bàn chơi trên điện thoại chỉ chơi ngang.
  - Chốt R0–R16; các chức năng bị loại chuyển sang mục 3.11.
- **0.1 (07/10/2026):** bản nháp đầu tiên.
