import React from "react";
import { Element, RenderElementProps } from "./sdui/Element";
import { Responsive } from "@/design-system/Responsive";
import { FontSize, FontWeight } from "@/design-system/Font";
import {
  buildFontSizeClassNames,
  buildFontWeightClassNames,
} from "@/design-system/FontStyle";

type TextAs = "p" | "h1" | "h2" | "h3" | "h4" | "h5" | "strong" | "i";

type TextProps = Element & {
  as?: TextAs;
  value: string;
  fontSize?: Responsive<FontSize>;
  fontWeight?: Responsive<FontWeight>;
};

const Comp = (tag?: TextAs) => {
  if (!tag) return "span";
  if (tag === "h1") return "h1";
  if (tag === "h2") return "h2";
  if (tag === "h3") return "h3";
  if (tag === "h4") return "h4";
  if (tag === "h5") return "h5";
  if (tag === "p") return "p";
  return "span";
};

const Text = (props: RenderElementProps<TextProps>) => {
  const Cmp = Comp(props.element.as);

  const result: string[] = [];

  result.push(buildFontSizeClassNames(props.element.fontSize));
  result.push(buildFontWeightClassNames(props.element.fontWeight));

  return <Cmp className={result.join(" ")} children={props.element.value} />;
};

export default Text;
