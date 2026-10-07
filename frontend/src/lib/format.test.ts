import { describe, expect, it } from "vitest";
import { formatSignedXu, formatXu } from "./format";

describe("formatXu", () => {
  it("groups thousands with a dot", () => {
    expect(formatXu(10200)).toBe("10.200 xu");
    expect(formatXu(1234567)).toBe("1.234.567 xu");
    expect(formatXu(0)).toBe("0 xu");
  });
});

describe("formatSignedXu", () => {
  it("always shows the sign and uses the real minus sign", () => {
    expect(formatSignedXu(300)).toBe("+300");
    expect(formatSignedXu(-200)).toBe("\u2212200");
    expect(formatSignedXu(-12500)).toBe("\u221212.500");
    expect(formatSignedXu(0)).toBe("0");
  });
});
