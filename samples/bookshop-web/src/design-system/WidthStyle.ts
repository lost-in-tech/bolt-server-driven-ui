import React, { CSSProperties } from "react";
import { Breakpoints } from "./Breakpoint";
import { Responsive } from "./Responsive";
import { Width } from "./Width";

export const WidthStyleCssMap: Record<Breakpoints, Record<Width, string>> = {
  xs: {
    full: "w-full",
    oneHalf: "w-1/2",
    oneThird: "w-1/3",
    twoThird: "w-2/3",
    oneFourth: "w-1/4",
    twoFourth: "w-2/4",
    threeFourth: "w-3/4",
    oneFifth: "w-1/5",
    twoFifth: "w-2/5",
    threeFifth: "w-3/5",
    fourFifth: "w-4/5",
    oneSixth: "w-1/6",
    twoSixth: "w-2/6",
    threeSixth: "w-3/6",
    fourSixth: "w-4/6",
    fiveSixth: "w-5/6",
  },
  sm: {
    full: "sm:w-full",
    oneHalf: "sm:w-1/2",
    oneThird: "sm:w-1/3",
    twoThird: "sm:w-2/3",
    oneFourth: "sm:w-1/4",
    twoFourth: "sm:w-2/4",
    threeFourth: "sm:w-3/4",
    oneFifth: "sm:w-1/5",
    twoFifth: "sm:w-2/5",
    threeFifth: "sm:w-3/5",
    fourFifth: "sm:w-4/5",
    oneSixth: "sm:w-1/6",
    twoSixth: "sm:w-2/6",
    threeSixth: "sm:w-3/6",
    fourSixth: "sm:w-4/6",
    fiveSixth: "sm:w-5/6",
  },
  md: {
    full: "md:w-full",
    oneHalf: "md:w-1/2",
    oneThird: "md:w-1/3",
    twoThird: "md:w-2/3",
    oneFourth: "md:w-1/4",
    twoFourth: "md:w-2/4",
    threeFourth: "md:w-3/4",
    oneFifth: "md:w-1/5",
    twoFifth: "md:w-2/5",
    threeFifth: "md:w-3/5",
    fourFifth: "md:w-4/5",
    oneSixth: "md:w-1/6",
    twoSixth: "md:w-2/6",
    threeSixth: "md:w-3/6",
    fourSixth: "md:w-4/6",
    fiveSixth: "md:w-5/6",
  },
  lg: {
    full: "lg:w-full",
    oneHalf: "lg:w-1/2",
    oneThird: "lg:w-1/3",
    twoThird: "lg:w-2/3",
    oneFourth: "lg:w-1/4",
    twoFourth: "lg:w-2/4",
    threeFourth: "lg:w-3/4",
    oneFifth: "lg:w-1/5",
    twoFifth: "lg:w-2/5",
    threeFifth: "lg:w-3/5",
    fourFifth: "lg:w-4/5",
    oneSixth: "lg:w-1/6",
    twoSixth: "lg:w-2/6",
    threeSixth: "lg:w-3/6",
    fourSixth: "lg:w-4/6",
    fiveSixth: "lg:w-5/6",
  },
};

export const buildWidthClassNames = (props?: Responsive<Width>): string => {
  if (!props) return "";
  const result: string[] = [];
  if (props.xs) result.push(WidthStyleCssMap["xs"][props.xs]);
  if (props.sm) result.push(WidthStyleCssMap["sm"][props.sm]);
  if (props.md) result.push(WidthStyleCssMap["md"][props.md]);
  if (props.lg) result.push(WidthStyleCssMap["lg"][props.lg]);

  return result.join(" ");
};

export const buildMinMaxWidthStyle = (
  min?: number,
  max?: number
): CSSProperties => {
  return {
    minWidth: min ? `${min}px` : "",
    maxWidth: max ? `${max}px` : "",
  };
};
