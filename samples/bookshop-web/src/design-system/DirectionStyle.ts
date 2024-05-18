import { Breakpoints } from "./Breakpoint";
import { Direction } from "./Direction";
import { Responsive } from "./Responsive";

export const FlexDirectionCssMap: Record<
  Breakpoints,
  Record<Direction, string>
> = {
  xs: {
    horizontal: "flex-row",
    vertical: "flex-col",
  },
  sm: {
    horizontal: "sm:flex-row",
    vertical: "sm:flex-col",
  },
  md: {
    horizontal: "md:flex-row",
    vertical: "md:flex-col",
  },
  lg: {
    horizontal: "lg:flex-row",
    vertical: "lg:flex-col",
  },
};

export const buildFlexDirectionClassNames = (
  props?: Responsive<Direction>,
  defaultCss?: string
): string => {
  if (!props) return defaultCss ?? "";
  const result: string[] = [];
  if (props.xs) result.push(FlexDirectionCssMap["xs"][props.xs]);
  if (props.sm) result.push(FlexDirectionCssMap["sm"][props.sm]);
  if (props.md) result.push(FlexDirectionCssMap["md"][props.md]);
  if (props.lg) result.push(FlexDirectionCssMap["lg"][props.lg]);

  return result.join(" ");
};
