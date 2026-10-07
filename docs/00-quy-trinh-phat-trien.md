# Quy trình phát triển

Cập nhật 07/10/2026

## Nhóm

| Người | Vai trò |
|---|---|
| Bạn | Chủ sản phẩm: quyết định nghiệp vụ, duyệt kết quả của từng bước |
| Claude | Phân tích, thiết kế, lập trình, kiểm thử, viết tài liệu |

## Các bước

| Bước | Nội dung | Kết quả | Trạng thái |
|---|---|---|---|
| 0 | Luật chơi | [luat-xi-dach.md](luat-xi-dach.md) | Đã chốt (0.7) |
| 1 | Phân tích yêu cầu: chức năng và phi chức năng | [01-phan-tich-yeu-cau.md](01-phan-tich-yeu-cau.md) | Đã chốt (1.0) |
| 2 | Thiết kế hệ thống: kiến trúc, cơ sở dữ liệu, API và kết nối thời gian thực, máy trạng thái của ván | [02-thiet-ke-he-thong.md](02-thiet-ke-he-thong.md) | Đã chốt (1.0) |
| 3 | Thiết kế giao diện: sơ đồ màn hình, wireframe, mockup | [03-thiet-ke-giao-dien.md](03-thiet-ke-giao-dien.md), [mockup](03-mockup/mockup.html) | Đã chốt (1.0) |
| 4 | Lập kế hoạch: danh sách việc (backlog), chia sprint | [04-ke-hoach.md](04-ke-hoach.md) | Đã chốt (1.0) |
| 5 | Dựng nền dự án (sprint S0): git, Docker Compose, CI | mã nguồn | **Đang làm** |
| 6 | Phát triển theo từng sprint | mã nguồn và kiểm thử | Chưa làm |
| 7 | Kiểm thử hệ thống: đầu-cuối, tải, bảo mật | báo cáo kiểm thử | Chưa làm |
| 8 | Triển khai: máy chủ, tên miền, HTTPS, sao lưu | hướng dẫn triển khai | Chưa làm; chỉ làm khi mọi thứ ổn định trên máy |
| 9 | Vận hành và bảo trì | | Chưa làm |

## Quy ước

- Mỗi bước chỉ kết thúc khi bạn duyệt ("chốt"). Chưa chốt bước trước thì không sang bước sau.
- Mỗi sprint ở bước 6 gồm: lập trình, kiểm thử tự động, demo cho bạn xem, bạn duyệt.
- Mỗi yêu cầu có một mã riêng (ví dụ `GAME-05`). Mã này được nhắc lại trong thiết kế, mã nguồn và tên ca kiểm thử để truy vết.
- Muốn đổi một yêu cầu đã chốt thì ghi vào mục "Lịch sử thay đổi" của tài liệu và đánh giá ảnh hưởng trước khi làm.
