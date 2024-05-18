import { Responsive } from "@/design-system/Responsive";
import { CSSProperties } from "react";

export const buildMedia = (propName: string, propVarName: string) => {
  return {
    "screen and (min-width: 468px)": {
      [propName]: `var(--local-${propVarName}-xs)`,
    },
    "screen and (min-width: 640px)": {
      [propName]: `var(--local-${propVarName}-sm)`,
    },
    "screen and (min-width: 768px)": {
      [propName]: `var(--local-${propVarName}-md)`,
    },
    "screen and (min-width: 1024px)": {
      [propName]: `var(--local-${propVarName}-lg)`,
    },
    "screen and (min-width: 1280px)": {
      [propName]: `var(--local-${propVarName}-xl)`,
    },
  };
};

const buildResponsive = <T extends any>(
  rsp?: Responsive<T>,
  defaultValue?: T
): Responsive<T> => {
  return {
    xs: rsp?.xs ?? defaultValue,
    sm: rsp?.sm ?? rsp?.xs ?? defaultValue,
    md: rsp?.md ?? rsp?.sm ?? rsp?.xs ?? defaultValue,
    lg: rsp?.lg ?? rsp?.md ?? rsp?.sm ?? rsp?.xs ?? defaultValue,
    xl: rsp?.xl ?? rsp?.lg ?? rsp?.md ?? rsp?.sm ?? rsp?.xs ?? defaultValue,
  } as Responsive<T>;
};

export const buildCssProperties = <T extends any>(
  name: string,
  defaultValue: T,
  input?: Responsive<T>,
  css?: (value?: T) => string | null
): CSSProperties => {
  const rsp = buildResponsive(input, defaultValue);
  return {
    [`--local-${name}-default`]: "column",
    [`--local-${name}-xs`]: css ? css(rsp.xs) : null,
    [`--local-${name}-sm`]: css ? css(rsp.sm) : null,
    [`--local-${name}-md`]: css ? css(rsp.md) : null,
    [`--local-${name}-lg`]: css ? css(rsp.lg) : null,
    [`--local-${name}-xl`]: css ? css(rsp.xl) : null,
  } as CSSProperties;
};
