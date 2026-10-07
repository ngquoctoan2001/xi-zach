import { cn } from "@/lib/utils";

type WordmarkProps = {
  size?: "sm" | "md" | "lg";
  className?: string;
};

/** "XÌ DÁCH" logo with the skewed "21" tag (design decision E4). */
export function Wordmark({ size = "md", className }: WordmarkProps) {
  return (
    <span role="img" aria-label="Xì Dách" className={cn("wordmark", `wordmark--${size}`, className)}>
      <b aria-hidden>XÌ DÁCH</b>
      <i aria-hidden>
        <span>21</span>
      </i>
    </span>
  );
}
