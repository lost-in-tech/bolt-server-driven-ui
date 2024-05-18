import { Breakpoints } from "./Breakpoint";
import { Color } from "./Color";
import { Responsive } from "./Responsive";

const BgColorCssMap: Record<Breakpoints, Record<Color, string>> = {
  xs: {
    primary: "bg-primary",
    "primary-content": "bg-primary-content",
    secondary: "bg-secondary",
    "secondary-content": "bg-secondary-content",
    accent: "bg-accent",
    "accent-content": "bg-accent-content",
    "base-100": "bg-base-100",
    "base-200": "bg-base-200",
    "base-300": "bg-base-300",
    "base-content": "bg-base-content",
    success: "bg-succeess",
    "success-content": "bg-success-content",
    info: "bg-info",
    "info-content": "bg-info-content",
    warning: "bg-warning",
    "warning-content": "bg-warning-content",
    error: "bg-error",
    "error-content": "bg-error-content",
  },
  sm: {
    primary: "sm:bg-primary",
    "primary-content": "sm:bg-primary-content",
    secondary: "sm:bg-secondary",
    "secondary-content": "sm:bg-secondary-content",
    accent: "sm:bg-accent",
    "accent-content": "sm:bg-accent-content",
    "base-100": "sm:bg-base-100",
    "base-200": "sm:bg-base-200",
    "base-300": "sm:bg-base-300",
    "base-content": "sm:bg-base-content",
    success: "sm:bg-succeess",
    "success-content": "sm:bg-success-content",
    info: "sm:bg-info",
    "info-content": "sm:bg-info-content",
    warning: "sm:bg-warning",
    "warning-content": "sm:bg-warning-content",
    error: "sm:bg-error",
    "error-content": "sm:bg-error-content",
  },
  md: {
    primary: "md:bg-primary",
    "primary-content": "md:bg-primary-content",
    secondary: "md:bg-secondary",
    "secondary-content": "md:bg-secondary-content",
    accent: "md:bg-accent",
    "accent-content": "md:bg-accent-content",
    "base-100": "md:bg-base-100",
    "base-200": "md:bg-base-200",
    "base-300": "md:bg-base-300",
    "base-content": "md:bg-base-content",
    success: "md:bg-succeess",
    "success-content": "md:bg-success-content",
    info: "md:bg-info",
    "info-content": "md:bg-info-content",
    warning: "md:bg-warning",
    "warning-content": "md:bg-warning-content",
    error: "md:bg-error",
    "error-content": "md:bg-error-content",
  },
  lg: {
    primary: "lg:bg-primary",
    "primary-content": "lg:bg-primary-content",
    secondary: "lg:bg-secondary",
    "secondary-content": "lg:bg-secondary-content",
    accent: "lg:bg-accent",
    "accent-content": "lg:bg-accent-content",
    "base-100": "lg:bg-base-100",
    "base-200": "lg:bg-base-200",
    "base-300": "lg:bg-base-300",
    "base-content": "lg:bg-base-content",
    success: "lg:bg-succeess",
    "success-content": "lg:bg-success-content",
    info: "lg:bg-info",
    "info-content": "lg:bg-info-content",
    warning: "lg:bg-warning",
    "warning-content": "lg:bg-warning-content",
    error: "lg:bg-error",
    "error-content": "lg:bg-error-content",
  },
};

const TextColorCssMap: Record<Breakpoints, Record<Color, string>> = {
  xs: {
    primary: "text-primary",
    "primary-content": "text-primary-content",
    secondary: "text-secondary",
    "secondary-content": "text-secondary-content",
    accent: "text-accent",
    "accent-content": "text-accent-content",
    "base-100": "text-base-100",
    "base-200": "text-base-200",
    "base-300": "text-base-300",
    "base-content": "text-base-content",
    success: "text-succeess",
    "success-content": "text-success-content",
    info: "text-info",
    "info-content": "text-info-content",
    warning: "text-warning",
    "warning-content": "text-warning-content",
    error: "text-error",
    "error-content": "text-error-content",
  },
  sm: {
    primary: "sm:text-primary",
    "primary-content": "sm:text-primary-content",
    secondary: "sm:text-secondary",
    "secondary-content": "sm:text-secondary-content",
    accent: "sm:text-accent",
    "accent-content": "sm:text-accent-content",
    "base-100": "sm:text-base-100",
    "base-200": "sm:text-base-200",
    "base-300": "sm:text-base-300",
    "base-content": "sm:text-base-content",
    success: "sm:text-succeess",
    "success-content": "sm:text-success-content",
    info: "sm:text-info",
    "info-content": "sm:text-info-content",
    warning: "sm:text-warning",
    "warning-content": "sm:text-warning-content",
    error: "sm:text-error",
    "error-content": "sm:text-error-content",
  },
  md: {
    primary: "md:text-primary",
    "primary-content": "md:text-primary-content",
    secondary: "md:text-secondary",
    "secondary-content": "md:text-secondary-content",
    accent: "md:text-accent",
    "accent-content": "md:text-accent-content",
    "base-100": "md:text-base-100",
    "base-200": "md:text-base-200",
    "base-300": "md:text-base-300",
    "base-content": "md:text-base-content",
    success: "md:text-succeess",
    "success-content": "md:text-success-content",
    info: "md:text-info",
    "info-content": "md:text-info-content",
    warning: "md:text-warning",
    "warning-content": "md:text-warning-content",
    error: "md:text-error",
    "error-content": "md:text-error-content",
  },
  lg: {
    primary: "lg:text-primary",
    "primary-content": "lg:text-primary-content",
    secondary: "lg:text-secondary",
    "secondary-content": "lg:text-secondary-content",
    accent: "lg:text-accent",
    "accent-content": "lg:text-accent-content",
    "base-100": "lg:text-base-100",
    "base-200": "lg:text-base-200",
    "base-300": "lg:text-base-300",
    "base-content": "lg:text-base-content",
    success: "lg:text-succeess",
    "success-content": "lg:text-success-content",
    info: "lg:text-info",
    "info-content": "lg:text-info-content",
    warning: "lg:text-warning",
    "warning-content": "lg:text-warning-content",
    error: "lg:text-error",
    "error-content": "lg:text-error-content",
  },
};

export const buildTextColorClassNames = (props?: Responsive<Color>) => {
  if (!props) return "";
  const result: string[] = [];

  if (props.xs) result.push(TextColorCssMap["xs"][props.xs]);
  if (props.sm) result.push(TextColorCssMap["sm"][props.sm]);
  if (props.md) result.push(TextColorCssMap["md"][props.md]);
  if (props.lg) result.push(TextColorCssMap["lg"][props.lg]);

  return result.join(" ");
};

export const buildBgColorClassNames = (props?: Responsive<Color>) => {
  if (!props) return "";
  const result: string[] = [];

  if (props.xs) result.push(BgColorCssMap["xs"][props.xs]);
  if (props.sm) result.push(BgColorCssMap["sm"][props.sm]);
  if (props.md) result.push(BgColorCssMap["md"][props.md]);
  if (props.lg) result.push(BgColorCssMap["lg"][props.lg]);

  return result.join(" ");
};
