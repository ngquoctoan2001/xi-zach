import { Wordmark } from "@/components/brand/wordmark";
import { ApiStatus } from "@/components/system/api-status";

export default function HomePage() {
  return (
    <main className="mx-auto flex min-h-dvh max-w-3xl flex-col justify-center gap-8 px-6 py-16">
      <Wordmark size="lg" />
      <section className="hud-panel p-6 sm:p-8">
        <p className="hud-kicker">Xì Dách · Sprint S0</p>
        <h1 className="mt-3 pt-1 font-display text-5xl leading-[1.15] font-black uppercase">Đang dựng nền dự án</h1>
        <p className="mt-3 text-text-2">
          Trang đăng nhập và sảnh sẽ có từ sprint S2. Trang này chỉ để kiểm tra bộ khung chạy đúng: giao diện, font
          tiếng Việt và kết nối tới máy chủ.
        </p>
        <ApiStatus className="mt-6" />
      </section>
    </main>
  );
}
