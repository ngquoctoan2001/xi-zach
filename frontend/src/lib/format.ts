const numberFormat = new Intl.NumberFormat("vi-VN");

/** 10200 → "10.200 xu" (design 2.6). */
export function formatXu(amount: number): string {
  return `${numberFormat.format(amount)} xu`;
}

/** Signed amounts always show a sign and use the real minus sign U+2212: "+300", "−200" (design 2.6). */
export function formatSignedXu(amount: number): string {
  if (amount === 0) return "0";
  const sign = amount > 0 ? "+" : "\u2212";
  return `${sign}${numberFormat.format(Math.abs(amount))}`;
}
