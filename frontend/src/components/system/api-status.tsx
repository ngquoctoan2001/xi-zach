"use client";

import { useCallback, useEffect, useState } from "react";
import { Button } from "@/components/ui/button";
import { cn } from "@/lib/utils";

type VersionInfo = { name: string; version: string; environment: string };

type Status = { state: "loading" } | { state: "ok"; info: VersionInfo } | { state: "error" };

/** Shows whether the browser can reach the API through the reverse proxy (sprint S0 smoke check). */
export function ApiStatus({ className }: { className?: string }) {
  const [status, setStatus] = useState<Status>({ state: "loading" });

  const check = useCallback(async () => {
    setStatus({ state: "loading" });
    try {
      const response = await fetch("/api/version", { cache: "no-store" });
      if (!response.ok) throw new Error(`HTTP ${response.status}`);
      setStatus({ state: "ok", info: (await response.json()) as VersionInfo });
    } catch {
      setStatus({ state: "error" });
    }
  }, []);

  useEffect(() => {
    // Defer to a microtask so the initial render never calls setState synchronously inside the effect.
    queueMicrotask(() => void check());
  }, [check]);

  return (
    <div
      role="status"
      aria-live="polite"
      className={cn(
        "flex flex-wrap items-center gap-3 rounded-xl border px-4 py-3 font-hud text-sm tracking-wide",
        status.state === "ok" && "border-win/40 bg-win/10 text-win",
        status.state === "error" && "border-lose/40 bg-lose/10 text-lose",
        status.state === "loading" && "border-stroke-2 bg-white/5 text-text-2",
        className,
      )}
    >
      {status.state === "loading" && <span>Đang kiểm tra máy chủ…</span>}
      {status.state === "ok" && (
        <span>
          Máy chủ hoạt động · phiên bản {status.info.version.split("+")[0]} · môi trường {status.info.environment}
        </span>
      )}
      {status.state === "error" && (
        <>
          <span>Không kết nối được máy chủ.</span>
          <Button size="sm" variant="outline" onClick={() => void check()}>
            Thử lại
          </Button>
        </>
      )}
    </div>
  );
}
