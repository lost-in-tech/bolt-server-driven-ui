import { Breakpoints } from "./Breakpoint";
import { FontWeight, FontSize } from "./Font";
import { Responsive } from "./Responsive";

const FontWeightCssMap: Record<Breakpoints, Record<FontWeight, string>> = {
  xs: {
    light: "font-light",
    regular: "font-normal",
    medium: "font-medium",
    semiBold: "font-semibold",
    bold: "font-bold",
    extraBold: "font-extrabold",
  },
  sm: {
    light: "sm:font-light",
    regular: "sm:font-normal",
    medium: "sm:font-medium",
    semiBold: "sm:font-semibold",
    bold: "sm:font-bold",
    extraBold: "sm:font-extrabold",
  },
  md: {
    light: "md:font-light",
    regular: "md:font-normal",
    medium: "md:font-medium",
    semiBold: "md:font-semibold",
    bold: "md:font-bold",
    extraBold: "md:font-extrabold",
  },
  lg: {
    light: "lg:font-light",
    regular: "lg:font-normal",
    medium: "lg:font-medium",
    semiBold: "lg:font-semibold",
    bold: "lg:font-bold",
    extraBold: "lg:font-extrabold",
  },
};

const FontSizeCssMap: Record<Breakpoints, Record<FontSize, string>> = {
  xs: {
    xs: "text-xs",
    sm: "text-sm",
    md: "text-base",
    lg: "text-lg",
    xl: "text-xl",
    twoXl: "text-2xl",
    threeXl: "text-3xl",
    fourXl: "text-4xl",
  },
  sm: {
    xs: "sm:text-xs",
    sm: "sm:text-sm",
    md: "sm:text-base",
    lg: "sm:text-lg",
    xl: "sm:text-xl",
    twoXl: "sm:text-2xl",
    threeXl: "sm:text-3xl",
    fourXl: "sm:text-4xl",
  },
  md: {
    xs: "md:text-xs",
    sm: "md:text-sm",
    md: "md:text-base",
    lg: "md:text-lg",
    xl: "md:text-xl",
    twoXl: "md:text-2xl",
    threeXl: "md:text-3xl",
    fourXl: "md:text-4xl",
  },
  lg: {
    xs: "lg:text-xs",
    sm: "lg:text-sm",
    md: "lg:text-base",
    lg: "lg:text-lg",
    xl: "lg:text-xl",
    twoXl: "lg:text-2xl",
    threeXl: "lg:text-3xl",
    fourXl: "lg:text-4xl",
  },
};

export const buildFontWeightClassNames = (props?: Responsive<FontWeight>) => {
  if (!props) return "";
  const result: string[] = [];

  if (props.xs) result.push(FontWeightCssMap["xs"][props.xs]);
  if (props.sm) result.push(FontWeightCssMap["sm"][props.sm]);
  if (props.md) result.push(FontWeightCssMap["md"][props.md]);
  if (props.lg) result.push(FontWeightCssMap["lg"][props.lg]);

  return result.join(" ");
};

export const buildFontSizeClassNames = (props?: Responsive<FontSize>) => {
  if (!props) return "";
  const result: string[] = [];

  if (props.xs) result.push(FontSizeCssMap["xs"][props.xs]);
  if (props.sm) result.push(FontSizeCssMap["sm"][props.sm]);
  if (props.md) result.push(FontSizeCssMap["md"][props.md]);
  if (props.lg) result.push(FontSizeCssMap["lg"][props.lg]);

  return result.join(" ");
};
