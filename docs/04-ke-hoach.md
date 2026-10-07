# Kế hoạch phát triển: game Xì Dách

Phiên bản 1.0 · 07/10/2026 · Trạng thái: **đã chốt**

Đây là bước 4 trong [quy trình phát triển](00-quy-trinh-phat-trien.md). Kế hoạch chia các yêu cầu đã chốt ở [phân tích yêu cầu 1.0](01-phan-tich-yeu-cau.md) thành từng sprint, theo [thiết kế hệ thống 1.0](02-thiet-ke-he-thong.md) và [thiết kế giao diện 1.0](03-thiet-ke-giao-dien.md).

## 1. Tóm tắt

| Mục | Nội dung |
|---|---|
| Mục tiêu | Phát hành bản 1.0 cho nhóm chơi nội bộ, gồm mọi chức năng Must và Should, đạt các yêu cầu phi chức năng |
| Phạm vi MVP | Mọi mục Must (quyết định R16) |
| Cách làm | 9 sprint (S0–S8); mỗi sprint kết thúc bằng một buổi demo và bạn duyệt |
| Nơi chạy thử | toàn bộ trên máy của bạn; bạn tự đóng nhiều người chơi để thử. Chỉ khi mọi thứ ổn định trên máy mới tính chuyện mua VPS và tên miền (P2, P3) |
| Thời gian dự kiến | khoảng 8–9 tuần làm việc, chưa tính thời gian chờ duyệt (mục 5) |
| Chi phí | chưa phát sinh khi chạy trên máy; sau này mới thuê VPS và mua tên miền (D1). Mọi thư viện, font, ảnh, âm thanh đều miễn phí |

## 2. Cách làm việc

### 2.1 Vai trò (RACI)

R = người làm · A = người quyết định cuối cùng · C = được hỏi ý kiến · I = được báo

| Việc | Bạn | Claude |
|---|---|---|
| Quyết định nghiệp vụ, luật chơi, ưu tiên | A, R | C |
| Viết tài liệu | A | R |
| Lập trình, kiểm thử tự động | I | A, R |
| Demo và nghiệm thu sprint | A | R |
| Tạo tài khoản GitHub; mua VPS, tên miền | A, R | C |
| Triển khai lên máy chủ | A | R |
| Vận hành hằng ngày (tạo tài khoản, nạp xu) | A, R | C |

### 2.2 Nhịp làm việc mỗi sprint

1. **Mở sprint:** mình gửi danh sách việc của sprint (lấy từ mục 4) và cách demo; bạn đồng ý hoặc chỉnh.
2. **Trong sprint:** mình làm từng việc theo thứ tự, báo ngắn trong chat khi xong mỗi việc lớn hoặc khi gặp vướng mắc cần bạn quyết.
3. **Đóng sprint:**
   - Mình gửi báo cáo gồm việc đã xong, việc còn lại, kết quả kiểm thử và hướng dẫn demo.
   - Bạn chơi thử hoặc xem demo rồi ghi "chốt sprint".
   - Chưa chốt sprint trước thì chưa sang sprint sau.
4. **Thay đổi yêu cầu giữa chừng:**
   - Ghi vào lịch sử thay đổi của tài liệu liên quan.
   - Mình đánh giá ảnh hưởng, rồi xếp việc mới vào sprint phù hợp.
   - Không chen ngang sprint đang làm, trừ lỗi nghiêm trọng.

### 2.3 Điều kiện để bắt đầu một việc (Definition of Ready)

- Có mã yêu cầu và tiêu chí chấp nhận, lấy từ tài liệu phân tích, luật chơi hoặc thiết kế.
- Các việc phụ thuộc đã xong, hoặc nằm trước nó trong cùng sprint.
- Việc có giao diện thì đã có wireframe hoặc mockup tham chiếu.
- Đã ước lượng cỡ việc.

### 2.4 Điều kiện để coi là xong (Definition of Done)

- **Mã nguồn:**
  - Đúng quy ước ở thiết kế hệ thống mục 12.
  - C#: bật nullable, cảnh báo coi là lỗi. TypeScript: chế độ strict. Lint không còn lỗi.
- **Kiểm thử:**
  - Có kiểm thử tự động cho việc vừa làm, tên ca kiểm thử bắt đầu bằng mã yêu cầu.
  - Thư viện luật chơi giữ độ bao phủ nhánh từ 95%.
- **CI:** chạy xanh, không có lỗ hổng thư viện mức High trở lên, không lộ bí mật.
- **Giao diện:**
  - Đúng thiết kế giao diện 1.0 (phong cách Meta Radar).
  - Trang bàn chơi dùng được trên máy tính và điện thoại xoay ngang.
  - Đạt các tiêu chí trợ năng cơ bản: dùng được bằng bàn phím, đủ tương phản, có nhãn cho trình đọc màn hình.
- **Tài liệu:** đã cập nhật nếu thay đổi API, cấu hình hoặc yêu cầu.
- **Gộp mã:** đã gộp vào nhánh main và chạy được trên máy bằng Docker Compose.

### 2.5 Cỡ việc

| Cỡ | Ý nghĩa |
|---|---|
| S | việc nhỏ, khoảng nửa ngày làm |
| M | khoảng 1–2 ngày làm |
| L | khoảng 3–5 ngày làm |

Cỡ việc dùng để chia sprint cho cân, không phải cam kết thời gian.

## 3. Chuẩn bị môi trường

| Thứ cần có | Tình trạng trên máy này | Ai lo |
|---|---|---|
| Git | có (2.54) | |
| .NET SDK 10 | có (10.0.201) | |
| Node.js 24 LTS | có (24.19) | |
| Docker và Docker Compose | có (29.7, Compose 5.5) | |
| GitHub CLI | chưa có; cài khi dựng nền dự án, hoặc dùng git kèm đăng nhập qua trình duyệt | Claude |
| Repo GitHub | có: github.com/ngquoctoan2001/xi-zach (P1) | bạn |
| VPS và tên miền | chưa cần; chỉ mua sau khi chạy ổn định trên máy (P2) | bạn |

## 4. Các sprint

Mỗi dòng là một việc trong backlog; cột "Yêu cầu" là mã trong tài liệu phân tích. Mục Should và Could được ghi rõ; còn lại là Must.

### S0 · Dựng nền dự án (bước 5)

**Mục tiêu:** khung dự án chạy được bằng 1 lệnh, CI chạy xanh.

| Mã việc | Việc | Yêu cầu | Cỡ |
|---|---|---|---|
| S0-01 | Khởi tạo git, repo GitHub riêng tư, quy ước nhánh và commit | MAIN-04, MAIN-06 | S |
| S0-02 | Solution .NET 10: 4 project và các project kiểm thử; bật analyzer, nullable, cảnh báo là lỗi | MAIN-01 | M |
| S0-03 | Next.js 16, TypeScript strict, Tailwind 4, shadcn/ui; bộ màu Meta Radar và 3 font | USA-01 | M |
| S0-04 | Docker Compose cho dev (db, seq, caddy) và khung compose production; file `.env.example` | OPS-04, SEC-06 | M |
| S0-05 | EF Core, migration đầu tiên, migration bundle; health check | MAIN-05, OPS-02 | S |
| S0-06 | CI GitHub Actions: build, lint, kiểm thử; quét lỗ hổng thư viện và quét bí mật | MAIN-04, SEC-06, SEC-07 | M |
| S0-07 | Serilog ghi JSON gửi về Seq, mã tương quan cho mỗi yêu cầu | OPS-01 | S |
| S0-08 | Tài nguyên: ảnh bài WebP 360×540, font, ghi giấy phép. Ảnh đại diện và âm thanh của Kenney sẽ tải ở S7, khi cần dùng | PERF-03 | S |
| S0-09 | Chặn công cụ tìm kiếm: `robots.txt`, thẻ `noindex` | LEGAL-03 | S |

**Demo:**
- `docker compose up` một lệnh là chạy.
- Trang trống hiện đúng màu và font Meta Radar.
- `/health/ready` báo ổn, CI chạy xanh.

### S1 · Bộ luật chơi (XiDach.Rules)

**Mục tiêu:** toàn bộ luật tính điểm, so bài, tính tiền đúng tuyệt đối và được kiểm thử đầy đủ.

| Mã việc | Việc | Yêu cầu | Cỡ |
|---|---|---|---|
| S1-01 | Lá bài, bộ bài, xào bằng CSPRNG theo Fisher–Yates; kiểm định chi-bình phương 1 triệu lần xào | FAIR-03 | S |
| S1-02 | Tính nút (A theo số lá), phân loại bài, đủ tuổi theo vai trò (luật 3, 4) | MAIN-01 | M |
| S1-03 | So bài con với cái, hệ số, cái được ưu tiên (luật 7, 8.1) | MAIN-01 | M |
| S1-04 | Tính tiền: trả thay cái, chia đều, giới hạn 12 lần, đền làng, không đủ xu (luật 8.2–8.4) | MAIN-01 | L |
| S1-05 | Mọi ví dụ trong luật thành ca kiểm thử; kiểm thử bất biến bằng FsCheck; độ bao phủ nhánh từ 95% | MAIN-02 | M |

**Demo:**
- Báo cáo kiểm thử và độ bao phủ.
- Mô phỏng 10.000 ván ngẫu nhiên, in bảng cho thấy tổng tiền mỗi ván luôn bằng 0 và không ai bị âm xu.

### S2 · Tài khoản và quản trị người dùng

**Mục tiêu:** QTV tạo được tài khoản; người chơi đăng nhập được và bị bắt đổi mật khẩu lần đầu.

| Mã việc | Việc | Yêu cầu | Cỡ |
|---|---|---|---|
| S2-01 | Identity, đăng nhập bằng cookie, khóa khi nhập sai, security stamp | AUTH-03, AUTH-07, SEC-01, SEC-02 | L |
| S2-02 | Ví xu và xu ban đầu; tạo sẵn 1 QTV và 5 người chơi, mỗi người chơi 10.000 xu | WAL-02, SYS-04 | M |
| S2-03 | Trang và API: đăng nhập, đăng xuất, đổi mật khẩu, bắt buộc đổi mật khẩu lần đầu | PUB-01, AUTH-04, AUTH-06, AUTH-12 | M |
| S2-04 | Tài khoản bị khóa: chặn đăng nhập, báo lý do, đăng xuất ngay nếu đang online | AUTH-08 | S |
| S2-05 | Bộ thành phần giao diện: nút, ô nhập, hộp thoại, toast, bảng dữ liệu, chip, logo chữ, khung trang người chơi và quản trị | USA-01, USA-06 | L |
| S2-06 | Quản trị người dùng: danh sách, tạo, chi tiết, đặt lại mật khẩu, khóa và mở khóa | ADM-01, ADM-03, ADM-04, ADM-07, ADM-15 | L |
| S2-07 | Nhật ký quản trị: ghi lại và trang xem | ADM-11 | M |
| S2-08 | Chống CSRF, giới hạn tần suất đăng nhập, header bảo mật; kiểm tra quyền ở mọi API | SEC-03, SEC-04, SEC-05, LEGAL-02 | S |

**Demo:**
- QTV tạo tài khoản và nhận mật khẩu tạm.
- Người chơi đăng nhập và đổi mật khẩu.
- QTV khóa tài khoản thì người chơi bị đăng xuất ngay.

### S3 · Ví xu

**Mục tiêu:** QTV nạp, trừ xu; sổ giao dịch luôn khớp.

| Mã việc | Việc | Yêu cầu | Cỡ |
|---|---|---|---|
| S3-01 | Sổ giao dịch chỉ thêm (có trigger chặn sửa, xóa) | REL-01 | M |
| S3-02 | Nạp và trừ xu; lệnh chờ, áp dụng sau khi ván kết thúc | ADM-06 | M |
| S3-03 | Trang ví xu của người chơi; tab giao dịch ở trang quản trị | WAL-01 | M |
| S3-04 | Đối soát xu hằng ngày, báo khi lệch | SYS-03 | S |

**Demo:**
- QTV nạp 5.000 xu, người chơi thấy giao dịch mới.
- Trừ quá số dư thì bị từ chối.
- Chạy đối soát cho kết quả khớp.

### S4 · Sảnh và phòng (thời gian thực)

**Mục tiêu:** nhiều người vào cùng một phòng, ngồi ghế và thấy nhau theo thời gian thực.

| Mã việc | Việc | Yêu cầu | Cỡ |
|---|---|---|---|
| S4-01 | GameHub: kết nối, xác thực, giới hạn tần suất lệnh, chống xử lý lặp | SEC-04, REL-03 | M |
| S4-02 | RoomManager, RoomActor: hàng đợi lệnh mỗi phòng, đồng hồ `TimeProvider` | MAIN-01 | L |
| S4-03 | Tạo phòng (tối đa 5 phòng, lưu bản sao cấu hình) | LOB-03 | M |
| S4-04 | Sảnh: danh sách phòng cập nhật trực tiếp; vào phòng bằng danh sách hoặc mã | LOB-01, LOB-04 | M |
| S4-05 | Vào phòng, ngồi ghế, đứng dậy, rời phòng, người xem (tối đa 20); mỗi tài khoản chỉ ở 1 phòng | ROOM-01, ROOM-02, ROOM-03, ROOM-05, AUTH-09 | M |
| S4-06 | Tự chuyển quyền chủ phòng khi chủ phòng rời; tự đóng phòng trống sau 5 phút | ROOM-08, ROOM-12 | S |
| S4-07 | Khung bàn chơi: 8 ghế trên máy tính và điện thoại xoay ngang, lời nhắc xoay ngang | ROOM-04, COMP-02 | L |

**Demo:** 3 trình duyệt (có 1 điện thoại) cùng vào một phòng, ngồi ghế, đứng dậy, thấy thay đổi của nhau ngay lập tức.

### S5 · Ván chơi (phía máy chủ)

**Mục tiêu:** chơi trọn một ván đúng luật, tiền và nhật ký ghi đúng.

| Mã việc | Việc | Yêu cầu | Cỡ |
|---|---|---|---|
| S5-01 | Chọn cái: hỏi nhận hoặc từ chối, không ai nhận, xin nghỉ, chế độ cố định và xoay tua, chủ phòng đổi cái | ROOM-06, ROOM-07, GAME-10 | L |
| S5-02 | Bắt đầu ván; đặt cược cố định và tự do | GAME-01, GAME-02 | M |
| S5-03 | Chia bài, kiểm tra xì bàn và xì dách, lượt con, lượt cái, xét | GAME-03, GAME-04, GAME-05, GAME-06 | L |
| S5-04 | Đồng hồ và tự chơi khi hết giờ; rớt mạng và kết nối lại | SYS-01, GAME-09 | M |
| S5-05 | Góc nhìn riêng cho từng người; kiểm thử chống lộ bài | FAIR-01, FAIR-02 | M |
| S5-06 | Tính tiền và ghi ván trong một giao dịch; nhật ký ván; hủy ván khi gián đoạn | GAME-07, GAME-11, GAME-12, REL-02, REL-04 | L |

**Demo:**
- 3 người chơi trọn một ván với bộ bài định sẵn, gồm cả trường hợp non và đền.
- Kiểm tra số dư, sổ giao dịch và nhật ký ván.
- Rút mạng một người giữa ván: game tự chơi thay.

### S6 · Bàn chơi hoàn chỉnh và quản trị ván — đạt MVP

**Mục tiêu:** đủ mọi mục Must, chơi thử được trên máy của bạn.

| Mã việc | Việc | Yêu cầu | Cỡ |
|---|---|---|---|
| S6-01 | Giao diện ván chơi: nút theo vai trò và pha, chọn mức cược, nút xét trên ghế, các hộp thoại (mời làm cái, xác nhận dằn non), bảng kết quả, băng mất kết nối | GAME-08, USA-02, USA-03, USA-04 | L |
| S6-02 | Phím tắt, vùng `aria-live`, vùng bấm 44 px | USA-05 | S |
| S6-03 | Trang hồ sơ (bản Must: tên, ảnh, số xu, ngày tham gia) | PROF-01 | S |
| S6-04 | Quản trị phòng: xem bàn ở dạng chỉ đọc, đóng phòng | ADM-08 | M |
| S6-05 | Tra cứu nhật ký ván | ADM-09 | M |

**Demo:**
- Bạn đóng 4–6 người chơi (nhiều trình duyệt, nhiều tài khoản), chơi nhiều ván trên máy tính và cả điện thoại nối cùng mạng wifi.
- QTV đóng phòng đang chơi: ván bị hủy và không ai mất xu.

### S7 · Các chức năng Should

**Mục tiêu:** hoàn thiện trải nghiệm theo các mục Should.

| Mã việc | Việc | Yêu cầu | Cỡ |
|---|---|---|---|
| S7-01 | Điều khoản ở lần đăng nhập đầu | PUB-03, LEGAL-01 | S |
| S7-02 | Trang hướng dẫn luật chơi | PUB-02 | M |
| S7-03 | Hoạt ảnh và âm thanh; tôn trọng giảm chuyển động | ROOM-11 | M |
| S7-04 | Sửa hồ sơ, cài đặt (âm thanh, không nhận làm cái), thống kê cá nhân | PROF-02, PROF-04, HIS-03 | M |
| S7-05 | Lịch sử ván | HIS-01 | M |
| S7-06 | Lọc sảnh; mật khẩu phòng và liên kết mời; chủ phòng tự chuyển quyền | LOB-02, LOB-04, ROOM-08 | M |
| S7-07 | Quản trị: bảng tổng quan, đổi vai trò, cấu hình có phiên bản, xóa tài khoản | ADM-02, ADM-05, ADM-10, ADM-16 | L |

**Demo:**
- Đi lại toàn bộ luồng đăng nhập lần đầu, có bước điều khoản.
- Chơi có âm thanh và hoạt ảnh.
- Xem lịch sử và thống kê.
- QTV đổi cấu hình rồi tạo phòng mới để thấy cấu hình mới được áp dụng.

### S8 · Kiểm thử hệ thống (bước 7)

**Mục tiêu:** chứng minh hệ thống đạt các yêu cầu phi chức năng trước khi phát hành.

| Mã việc | Việc | Yêu cầu | Cỡ |
|---|---|---|---|
| S8-01 | Kiểm thử đầu-cuối bằng Playwright trên Chromium, Firefox, WebKit | MAIN-03, COMP-01 | M |
| S8-02 | Kiểm thử tải: bot chơi 10 phòng, đo độ trễ phân vị 95 | PERF-01, PERF-02, PERF-04 | M |
| S8-03 | Quét bảo mật OWASP ZAP; rà lại phân quyền | SEC-04, SEC-05 | M |
| S8-04 | Rà trợ năng (Lighthouse, bàn phím, trình đọc màn hình); đo LCP | USA-05, PERF-03 | S |
| S8-05 | Bảng giám sát Seq, cảnh báo; diễn tập sao lưu và khôi phục | OPS-03, REL-06 | M |
| S8-06 | Sửa các lỗi tìm được | | M |

**Demo:** báo cáo kiểm thử hệ thống (đạt / không đạt theo từng yêu cầu phi chức năng).

### Sau S8 · Triển khai (bước 8) và vận hành (bước 9)

- **Triển khai lên production:**
  - Cài VPS, Caddy và HTTPS.
  - Chạy dữ liệu khởi tạo.
  - Bật sao lưu ra ngoài máy chủ (D3) và giám sát uptime [REL-05].
  - Đổi mật khẩu của 6 tài khoản tạo sẵn.
- **Cập nhật về sau:** đi theo quy trình bật chế độ bảo trì [OPS-05].

### Backlog Could (chưa xếp sprint)

Làm sau bản 1.0 nếu bạn muốn:

| Yêu cầu | Chức năng |
|---|---|
| PROF-03 | xem hồ sơ người khác |
| LOB-05 | chơi nhanh |
| ROOM-09 | mời người ra khỏi phòng |
| HIS-02 | xem lại một ván |
| ADM-12 | thông báo hệ thống |
| ADM-13 | chế độ bảo trì; nên làm sớm nếu muốn cập nhật không cắt ngang ván |
| FAIR-04 | minh bạch ván bằng mã băm bộ bài |
| FAIR-05 | cảnh báo nhiều tài khoản cùng IP ngồi chung bàn |

## 5. Mốc và thời gian dự kiến

| Mốc | Xong khi | Dự kiến (tính từ lúc chốt kế hoạch) |
|---|---|---|
| M1 · Nền dự án | hết S0 | ngày 3 |
| M2 · Bộ luật đúng và đủ test | hết S1 | tuần 1 |
| M3 · Đăng nhập, quản trị người dùng, ví xu | hết S3 | tuần 3 |
| M4 · Chơi trọn một ván nhiều người | hết S5 | tuần 5 |
| M5 · MVP: đủ mọi mục Must, chơi thử trên máy | hết S6 | tuần 6 |
| M6 · Đủ các mục Should | hết S7 | tuần 7 |
| M7 · Kiểm thử hệ thống đạt | hết S8 | tuần 8 |
| M8 · Phát hành bản 1.0 | sau bước 8, khi bạn quyết định mua VPS và tên miền | tùy bạn |

Thời gian chưa tính lúc chờ bạn duyệt demo, và có thể dời nếu yêu cầu thay đổi.

## 6. Rủi ro

Khả năng (KN) và tác động (TĐ) chấm từ 1 đến 3; điểm bằng KN × TĐ.

| Mã | Rủi ro | KN | TĐ | Điểm | Cách xử lý |
|---|---|---|---|---|---|
| K1 | Tính tiền sai ở các trường hợp hiếm (trả thay cái, giới hạn 12 lần, không đủ xu) làm sai số dư | 2 | 3 | 6 | Làm luật trước tiên (S1); kiểm thử bất biến trên hàng nghìn ván ngẫu nhiên; đối soát hằng ngày |
| K2 | Lỗi đồng bộ thời gian thực (2 lệnh cùng lúc, rớt mạng giữa lượt) | 2 | 3 | 6 | Mỗi phòng một hàng đợi lệnh; kiểm thử bằng đồng hồ giả; chống xử lý lặp |
| K3 | Bàn 8 ghế bị chật trên điện thoại xoay ngang | 2 | 2 | 4 | Dựng khung bàn sớm ở S4 và thử trên điện thoại thật (nối wifi tới máy của bạn) ngay sprint đó |
| K4 | Yêu cầu thay đổi giữa chừng làm trễ kế hoạch | 2 | 2 | 4 | Ghi vào lịch sử thay đổi; đánh giá ảnh hưởng; xếp vào sprint sau, không chen ngang |
| K5 | Mất dữ liệu khi hỏng VPS | 1 | 3 | 3 | Sao lưu hằng ngày ra ngoài máy chủ; diễn tập khôi phục ở S8 |
| K6 | Rủi ro pháp lý (Nghị định 147/2024) nếu game lan ra ngoài nhóm | 1 | 3 | 3 | Chỉ dùng nội bộ: không đăng ký tự do, chặn công cụ tìm kiếm, không kinh doanh |
| K7 | Chờ duyệt lâu làm dừng tiến độ | 2 | 1 | 2 | Gom demo gọn; mỗi demo có hướng dẫn từng bước |
| K8 | Không đạt độ trễ 300 ms khi có tải | 1 | 2 | 2 | Kiến trúc đơn giản, xử lý trong bộ nhớ; đo ở S8, còn thời gian để sửa |
| K9 | Mất mạch công việc giữa các phiên làm việc của Claude | 2 | 1 | 2 | Tài liệu trong `docs` là nguồn sự thật; commit thường xuyên; mỗi sprint có báo cáo |

## 7. Các quyết định đã chốt

| Mã | Câu hỏi | Quyết định |
|---|---|---|
| P1 | Repo GitHub | https://github.com/ngquoctoan2001/xi-zach. Lần đầu đẩy mã, git mở trình duyệt để bạn đăng nhập GitHub một lần trên máy này. |
| P2 | Khi nào mua VPS và tên miền? | Chưa mua. Chạy và thử hết trên máy của bạn trước; ổn định, kiểm thử xong mới tính. |
| P3 | Demo ở đâu? | Mọi sprint đều chạy trên máy của bạn (Docker Compose). Bạn tự đóng nhiều người chơi bằng nhiều trình duyệt, nhiều tài khoản; điện thoại nối cùng wifi để thử màn hình ngang. |
| P4 | Có đưa MVP lên production sớm không? | Không. Production chỉ tính sau khi mọi thứ chạy ổn định trên máy. |

## 8. Lịch sử thay đổi

- **1.0 (07/10/2026):** chốt bước 4. Đổi sang chạy thử hoàn toàn trên máy của bạn; bỏ staging trên VPS cho tới khi ổn định; ảnh đại diện và âm thanh dời sang S7.
- **0.1 (07/10/2026):** bản nháp đầu tiên.
