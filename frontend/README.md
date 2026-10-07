# Frontend Xì Dách (Next.js 16)

Cách chạy và kiểm thử: xem [README ở thư mục gốc](../README.md).

| Lệnh | Việc làm |
|---|---|
| `npm run dev` | chạy chế độ phát triển ở cổng 3000 (mở qua Caddy: http://localhost:8080) |
| `npm run lint` | kiểm tra ESLint |
| `npm run typecheck` | sinh kiểu cho route rồi chạy `tsc` |
| `npm test` | chạy Vitest |
| `npm run build` | build bản standalone cho Docker |
| `npm run format` | định dạng mã bằng Prettier |

- **Giao diện:** theo [thiết kế giao diện 1.0](../docs/03-thiet-ke-giao-dien.md), phong cách Meta Radar. Bộ màu khai báo trong `src/app/globals.css`.
- **Ảnh bài:** `public/cards/*.webp` (360×540), xuất từ `assets/cards`.
