import { StackWidth } from "@/types";
import { UiSpace } from "@/types/UiSpace.type";
import { FontSize } from "@/types/fontSize.type";
import { defineProperties, createSprinkles } from "@vanilla-extract/sprinkles";

export const responsiveWidthMap = {
  [0]: "inherit",
  [8]: "8px",
  [16]: "16px",
  [32]: "32px",
  [64]: "64px",
  [128]: "128px",
  [150]: "150px",
  [200]: "200px",
  [250]: "250px",
  [300]: "300px",
  [350]: "350px",
  [400]: "400px",
  [450]: "450px",
  [468]: "468px",
  [500]: "500px",
  [550]: "550px",
  [600]: "600px",
  [640]: "640px",
  [650]: "650px",
  [700]: "700px",
  [750]: "750px",
  [768]: "768px",
  [800]: "800px",
  [850]: "850px",
  [900]: "900px",
  [950]: "950px",
  [1000]: "1000px",
  [1024]: "1024px",
} as Record<number, string>;

export const responsiveStackWidthPercentMap = {
  ["inherit"]: "inherit",
  [StackWidth.full]: "100%",
  [StackWidth.oneHalf]: "50%",
  [StackWidth.oneThird]: "33.3333%",
  [StackWidth.oneFourth]: "25%",
  [StackWidth.oneFifth]: "20%",
  [StackWidth.oneSixth]: "16.6666%",
  [StackWidth.oneSeventh]: "14.2857%",
  [StackWidth.oneEighth]: "12.5%",
  [StackWidth.oneNinth]: "11.1111%",
  [StackWidth.oneTenth]: "10%",
} as Record<string, string>;

export const responsiveSpacing = {
  [UiSpace.none]: "0px",
  [UiSpace.tenXs]: "1px",
  [UiSpace.nineXs]: "2px",
  [UiSpace.eightXs]: "3px",
  [UiSpace.sevenXs]: "4px",
  [UiSpace.sixXs]: "5px",
  [UiSpace.fiveXs]: "6px",
  [UiSpace.fourXs]: "8px",
  [UiSpace.threeXs]: "10px",
  [UiSpace.twoXs]: "12px",
  [UiSpace.xs]: "14px",
  [UiSpace.md]: "16px",
  [UiSpace.lg]: "18px",
  [UiSpace.xl]: "20px",
  [UiSpace.twoXl]: "22px",
  [UiSpace.threeXl]: "24px",
  [UiSpace.fourXl]: "26px",
  [UiSpace.fiveXl]: "28px",
  [UiSpace.sixXl]: "30px",
} as Record<UiSpace, string>;

export const responsiveFontSize = {
  [FontSize.twoXs]: "11px",
  [FontSize.xs]: "12px",
  [FontSize.sm]: "14px",
  [FontSize.md]: "16px",
  [FontSize.lg]: "18px",
  [FontSize.xl]: "20px",
  [FontSize.twoXl]: "22px",
  [FontSize.threeXl]: "24px",
  [FontSize.fourXl]: "26px",
  [FontSize.fiveXl]: "28px",
  [FontSize.sixXl]: "30px",
  [FontSize.sevenXl]: "32px",
  [FontSize.eightXl]: "34px",
  [FontSize.nineXl]: "36px",
  [FontSize.tenXl]: "38px",
  [FontSize.elevenXl]: "40px",
} as Record<FontSize, string>;

const responsiveProperties = defineProperties({
  conditions: {
    default: {},
    xs: { "@media": "screen and (min-width: 468px)" },
    sm: { "@media": "screen and (min-width: 640px)" },
    md: { "@media": "screen and (min-width: 768px)" },
    lg: { "@media": "screen and (min-width: 1024px)" },
  },
  defaultCondition: "default",
  responsiveArray: ["default", "xs", "sm", "md", "lg"],
  properties: {
    display: ["flex", "block", "none", "flex-inline"],
    flexDirection: ["column", "row"],
    width: Object.values(responsiveStackWidthPercentMap),
    minWidth: Object.values(responsiveWidthMap),
    maxWidth: Object.values(responsiveWidthMap),
    paddingLeft: Object.values(responsiveSpacing),
    paddingRight: Object.values(responsiveSpacing),
    paddingTop: Object.values(responsiveSpacing),
    paddingBottom: Object.values(responsiveSpacing),
    fontSize: Object.values(responsiveFontSize),
    gap: Object.values(responsiveSpacing),
  },
});

const colors = {
  "bg-primary": "oklch(0.3745 0.189 325.02)",
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
};

const colorProperties = defineProperties({
  conditions: {
    lightMode: {},
    darkMode: { "@media": "(prefers-color-scheme: dark)" },
  },
  defaultCondition: "lightMode",
  properties: {
    color: colors,
    background: colors,
  },
});

export const sprinkles = createSprinkles(responsiveProperties, colorProperties);

export type Sprinkles = Parameters<typeof sprinkles>[0];
