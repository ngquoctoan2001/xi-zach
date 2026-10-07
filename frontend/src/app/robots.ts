import type { MetadataRoute } from "next";

// LEGAL-03: the game is for a private group only, so no crawler may index any page.
export default function robots(): MetadataRoute.Robots {
  return { rules: { userAgent: "*", disallow: "/" } };
}
