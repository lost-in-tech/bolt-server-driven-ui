import { Breakpoints } from "./Breakpoint";
import { Responsive } from "./Responsive";
import { Space } from "./Space";

export const GapStyleCssMap: Record<Breakpoints, Record<Space, string>> = {
  xs: {
    none: "gap-0",
    xs: "gap-2",
    sm: "gap-4",
    md: "gap-6",
    lg: "gap-8",
    xl: "gap-10",
    twoXl: "gap-12",
  },
  sm: {
    none: "sm:gap-0",
    xs: "sm:gap-2",
    sm: "sm:gap-4",
    md: "sm:gap-6",
    lg: "sm:gap-8",
    xl: "sm:gap-10",
    twoXl: "sm:gap-12",
  },
  md: {
    none: "md:gap-0",
    xs: "md:gap-2",
    sm: "md:gap-4",
    md: "md:gap-6",
    lg: "md:gap-8",
    xl: "md:gap-10",
    twoXl: "md:gap-12",
  },
  lg: {
    none: "lg:gap-0",
    xs: "lg:gap-2",
    sm: "lg:gap-4",
    md: "lg:gap-6",
    lg: "lg:gap-8",
    xl: "lg:gap-10",
    twoXl: "lg:gap-12",
  },
};

export const buildGapClassNames = (props?: Responsive<Space>): string => {
  if (!props) return "gap-0";
  const result: string[] = [];
  if (props.xs) result.push(GapStyleCssMap["xs"][props.xs]);
  if (props.sm) result.push(GapStyleCssMap["sm"][props.sm]);
  if (props.md) result.push(GapStyleCssMap["md"][props.md]);
  if (props.lg) result.push(GapStyleCssMap["lg"][props.lg]);

  return result.join(" ");
};
