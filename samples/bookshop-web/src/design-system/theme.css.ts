import { createGlobalTheme } from "@vanilla-extract/css";

export const vars = createGlobalTheme(":root", {
  breakpoints: {
    xs: "468",
    sm: "640",
    md: "768",
    lg: "1024",
    xl: "1280",
  },
  colors: {
    bgPrimary: "oklch(0.3745 0.189 325.02)",
    bgPrimaryContent: "oklch(0.8749 0.0378 325.02)",
    bgSecondary: "oklch(0.5392 0.162 241.36)",
    bgSecondaryContent: "oklch(0.90784 0.0324 241.36)",
    bgAccent: "oklch(0.7598 0.204 56.72)",
    bgAccentContent: "oklch(0.15196 0.0408 56.72)",
    bgNeutral: "oklch(0.278078 0.029596 256.848)",
    bgNeutralContent: "oklch(0.855616 0.005919 256.848)",

    base100: "oklch(1 0 0)",
    base200: "oklch(0.93 0 0)",
    base300: "oklch(0.86 0 0)",
    baseContent: "oklch(0.278078 0.029596 256.848)",

    info: "oklch(0.7206 0.191 231.6)",
    infoContent: "oklch(0 0 0)",
    success: "oklch(0.648 0.15 160)",
    successContent: "oklch(0 0 0)",

    warning: "oklch(0.8471 0.199 83.87)",
    warningContent: "oklch(0 0 0)",
    error: "oklch(0.7176 0.221 22.18)",
    errorContent: "oklch(0 0 0)",
  },
  fontNames: {
    default: "Roboto, Arial, sans-serif",
  },
  fontSizes: {
    xs: "12px",
    sm: "14px",
    md: "16px",
    lg: "18px",
    xl: "20px",
    twoXl: "22px",
    threeXl: "26px",
    fourXl: "30px",
  },
  fontWeight: {
    normal: "normal",
    medium: "500",
    bold: "bold",
  },
});
