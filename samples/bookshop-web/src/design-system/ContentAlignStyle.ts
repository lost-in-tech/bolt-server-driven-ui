import { Breakpoints } from "./Breakpoint";
import { ContentAlign, ContentAlignX, ContentAlignY } from "./ContentAlign";

export const ContentAlignXCssMap: Record<
  Breakpoints,
  Record<ContentAlignX, string>
> = {
  xs: {
    left: "justify-start",
    right: "justify-end",
    center: "justify-center",
  },
  sm: {
    left: "sm:justify-start",
    right: "sm:justify-end",
    center: "sm:justify-center",
  },
  md: {
    left: "md:justify-start",
    right: "md:justify-end",
    center: "md:justify-center",
  },
  lg: {
    left: "lg:justify-start",
    right: "lg:justify-end",
    center: "lg:justify-center",
  },
};

export const ContentAlignYCssMap: Record<
  Breakpoints,
  Record<ContentAlignY, string>
> = {
  xs: {
    top: "content-start",
    bottom: "content-end",
    center: "content-center",
  },
  sm: {
    top: "sm:content-start",
    bottom: "sm:content-end",
    center: "sm:content-center",
  },
  md: {
    top: "md:content-start",
    bottom: "md:content-end",
    center: "md:content-center",
  },
  lg: {
    top: "lg:content-start",
    bottom: "lg:content-end",
    center: "lg:content-center",
  },
};

export const buildContentAlignClassNames = (props?: ContentAlign): string => {
  if (!props) return "border-0";
  const result: string[] = [];

  if (props.contentAlignX) {
    if (props.contentAlignX?.xs)
      result.push(ContentAlignXCssMap["xs"][props.contentAlignX.xs]);
    if (props.contentAlignX?.sm)
      result.push(ContentAlignXCssMap["sm"][props.contentAlignX.sm]);
    if (props.contentAlignX?.md)
      result.push(ContentAlignXCssMap["md"][props.contentAlignX.md]);
    if (props.contentAlignX?.lg)
      result.push(ContentAlignXCssMap["lg"][props.contentAlignX.lg]);
  }

  if (props.contentAlignY) {
    if (props.contentAlignY?.xs)
      result.push(ContentAlignYCssMap["xs"][props.contentAlignY.xs]);
    if (props.contentAlignY?.sm)
      result.push(ContentAlignYCssMap["sm"][props.contentAlignY.sm]);
    if (props.contentAlignY?.md)
      result.push(ContentAlignYCssMap["md"][props.contentAlignY.md]);
    if (props.contentAlignY?.lg)
      result.push(ContentAlignYCssMap["lg"][props.contentAlignY.lg]);
  }

  return result.join(" ");
};
