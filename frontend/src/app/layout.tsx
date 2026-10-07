import type { Metadata } from "next";
import { Be_Vietnam_Pro, Chakra_Petch, Saira_Extra_Condensed } from "next/font/google";
import "./globals.css";

// The three Meta Radar fonts, all with the Vietnamese subset (design 2.2).
const beVietnam = Be_Vietnam_Pro({
  variable: "--font-be-vietnam",
  subsets: ["latin", "vietnamese"],
  weight: ["400", "500", "600", "700", "800"],
});

const chakra = Chakra_Petch({
  variable: "--font-chakra",
  subsets: ["latin", "vietnamese"],
  weight: ["500", "600", "700"],
  style: ["normal", "italic"],
});

const saira = Saira_Extra_Condensed({
  variable: "--font-saira",
  subsets: ["latin", "vietnamese"],
  weight: ["600", "700", "800", "900"],
});

export const metadata: Metadata = {
  title: { default: "Xì Dách", template: "%s · Xì Dách" },
  description: "Game Xì Dách chơi nội bộ trong nhóm.",
  // LEGAL-03: private game, keep it out of search engines.
  robots: { index: false, follow: false },
};

export default function RootLayout({ children }: LayoutProps<"/">) {
  return (
    <html lang="vi" className={`dark ${beVietnam.variable} ${chakra.variable} ${saira.variable}`}>
      <body>{children}</body>
    </html>
  );
}
