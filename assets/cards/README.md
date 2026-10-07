# Bộ bài tây: 52 lá + mặt sau

```
assets/cards/
├── svg/         52 lá + back.svg, file vector gốc 360×540 (phóng to bao nhiêu cũng nét)
├── png/         52 lá + back.png, 720×1080, PNG nền trong suốt (góc bo tròn)
└── preview.png  ảnh xem nhanh cả bộ
```

## Cách đặt tên file

`<hạng><chất>`, ví dụ `AS` = Át Bích, `10H` = 10 Cơ, `QD` = Q Rô, `back` = mặt sau.

| Mã chất | Chất | Tên Việt |
|---|---|---|
| `S` | ♠ Spades | Bích |
| `C` | ♣ Clubs | Chuồn (Tép) |
| `D` | ♦ Diamonds | Rô |
| `H` | ♥ Hearts | Cơ |

Hạng: `A 2 3 4 5 6 7 8 9 10 J Q K`

## Nguồn và giấy phép

Cả mặt trước lẫn mặt sau đều dùng giấy phép **CC0 (public domain)**: được dùng miễn phí, kể cả cho mục đích thương mại, được sửa tùy ý và không bắt buộc ghi công.

- **52 mặt trước:** bộ "English pattern playing cards" của Dmitry Fomin, lấy từ Wikimedia Commons:
  https://commons.wikimedia.org/wiki/Category:SVG_English_pattern_playing_cards
  Các lá được tách từ file gốc của tác giả có chứa cả bộ, `English pattern playing cards deck.svg` (đã đối chiếu mã SHA-1 với Commons), và chỉ dịch về gốc tọa độ, không chỉnh sửa hình vẽ.
- **Mặt sau:** mẫu "design 3" (màu đỏ) trong bộ "Playing cards back vector svg" của BdRGames (Bas de Reuver), lấy từ OpenGameArt:
  https://opengameart.org/content/playing-cards-back-vector-svg
  Mẫu gốc có kích thước 250×350. Mình đã dựng lại theo khung 360×540, dùng đúng đường viền và độ bo góc của mặt trước. Hình chỉ phóng theo tỉ lệ đều, không bị kéo méo.

## Ghi chú kỹ thuật

- Kích thước gốc là 360×540 (tỉ lệ 2:3). Các file PNG được render ở mức 2x bằng Chrome headless.
- Nếu cần kích thước khác, render lại từ file SVG thay vì phóng to file PNG.
