# Xì Dách

Game Xì Dách nhiều người chơi trên trình duyệt, dùng nội bộ trong nhóm, chơi bằng xu ảo.

- **Frontend:** Next.js 16 (thư mục `frontend`)
- **Backend:** .NET 10 (thư mục `backend`)
- **Cơ sở dữ liệu:** PostgreSQL 18
- **Đóng gói:** Docker Compose (thư mục `deploy`)

Mọi quyết định về luật chơi, yêu cầu và thiết kế nằm trong thư mục [`docs`](docs/00-quy-trinh-phat-trien.md).

## Cấu trúc thư mục

```
docs/        tài liệu theo từng bước (luật chơi, yêu cầu, thiết kế, kế hoạch)
assets/      ảnh bài gốc (SVG, PNG)
backend/     XiDach.Rules, XiDach.Game, XiDach.Infrastructure, XiDach.Api và các project kiểm thử
frontend/    giao diện Next.js
deploy/      compose.yaml (chạy đủ bộ), compose.dev.yaml (phát triển), Caddyfile
```

## Chạy trên máy

Chỉ cần cài Docker Desktop.

1. Sao chép `deploy/.env.example` thành `deploy/.env`, rồi đổi `POSTGRES_PASSWORD` thành một chuỗi dài ngẫu nhiên.
2. Chạy:

   ```bash
   docker compose -f deploy/compose.yaml up -d --build
   ```

3. Mở game: <http://localhost:8080>
4. Xem log (Seq): <http://localhost:5341>

Tắt hệ thống:

```bash
docker compose -f deploy/compose.yaml down
```

Thêm `-v` vào lệnh tắt nếu muốn xóa luôn dữ liệu.

### Tự đóng nhiều người chơi trên một máy

Mỗi tài khoản cần một nơi lưu đăng nhập (cookie) riêng. Có thể dùng một trong các cách sau:

- Mở bằng nhiều trình duyệt khác nhau (Chrome, Edge, Firefox).
- Mở bằng nhiều hồ sơ (profile) của Chrome.
- Mở cùng một trình duyệt nhưng bằng các địa chỉ khác nhau. Mỗi địa chỉ giữ một phiên đăng nhập riêng:
  - `http://localhost:8080`
  - `http://127.0.0.1:8080`
  - `http://<IP-mạng-LAN-của-máy>:8080`

Để thử trên điện thoại, nối điện thoại vào cùng wifi rồi mở `http://<IP-mạng-LAN-của-máy>:8080`.

## Phát triển (tự nạp lại khi sửa mã)

```bash
docker compose -f deploy/compose.dev.yaml up -d
```

```bash
dotnet watch --project backend/src/XiDach.Api
```

```bash
npm --prefix frontend run dev
```

Mở <http://localhost:8080>. Caddy chuyển `/api` sang API ở cổng 5080, còn lại sang Next.js ở cổng 3000, giống hệt khi chạy đủ bộ.

## Kiểm thử

```bash
dotnet test backend/XiDach.slnx
```

```bash
npm --prefix frontend run lint && npm --prefix frontend run typecheck && npm --prefix frontend test
```

CI trên GitHub Actions chạy đầy đủ các bước trên, kèm quét lỗ hổng thư viện và quét bí mật.

## Giấy phép tài nguyên

Xem [THIRD_PARTY_NOTICES.md](THIRD_PARTY_NOTICES.md).
