import React, { CSSProperties } from "react";
import { Breakpoints } from "./Breakpoint";
import { Responsive } from "./Responsive";
import { Height } from "./Height";

export const HeightStyleCssMap: Record<Breakpoints, Record<Height, string>> = {
  xs: {
    full: "h-full",
    screen: "h-screen",
    fit: "h-fit",
  },
  sm: {
    full: "sm:h-full",
    screen: "sm:h-screen",
    fit: "sm:h-fit",
  },
  md: {
    full: "md:h-full",
    screen: "md:h-screen",
    fit: "md:h-fit",
  },
  lg: {
    full: "lg:h-full",
    screen: "lg:h-screen",
    fit: "lg:h-fit",
  },
};

export const buildHeightClassNames = (props?: Responsive<Height>): string => {
  if (!props) return "";
  const result: string[] = [];
  if (props.xs) result.push(HeightStyleCssMap["xs"][props.xs]);
  if (props.sm) result.push(HeightStyleCssMap["sm"][props.sm]);
  if (props.md) result.push(HeightStyleCssMap["md"][props.md]);
  if (props.lg) result.push(HeightStyleCssMap["lg"][props.lg]);

  return result.join(" ");
};

export const buildMinMaxHeightStyle = (
  min?: number,
  max?: number
): CSSProperties => {
  return {
    minHeight: min ? `${min}px` : "",
    maxHeight: max ? `${max}px` : "",
  };
};
