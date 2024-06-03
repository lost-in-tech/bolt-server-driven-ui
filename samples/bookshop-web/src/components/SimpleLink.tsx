import { Color } from "@/design-system/Color";
import { buildTextColorClassNames } from "@/design-system/ColorStyle";
import { FontSize } from "@/design-system/Font";
import { buildFontSizeClassNames } from "@/design-system/FontStyle";
import { Responsive } from "@/design-system/Responsive";
import { Element, RenderElementProps } from "./sdui/Element";
import Link from "next/link";

type SimpleLinkProps = Element & {
  url: string;
  target: "self" | "window";
  text: string;
  title: string;
  textColor?: Responsive<Color>;
  fontSize?: Responsive<FontSize>;
  external?: false;
};

const TargetMap: Record<string, string> = {
  self: "_slef",
  window: "_blank",
};

const buildClassNames = (props: SimpleLinkProps) => {
  const result: string[] = [];

  const fontColor = buildTextColorClassNames(props.textColor);
  const fontSize = buildFontSizeClassNames(props.fontSize);

  if (fontColor) result.push(fontColor);
  if (fontSize) result.push(fontSize);

  return result.join(" ");
};

export const SimpleLink = (props: RenderElementProps<SimpleLinkProps>) => {
  const className = buildClassNames(props.element);
  const finalClassName = `text-base-content hover:underline ${className}`;
  const target = TargetMap[props.element.target] ?? "_self";
  if (props.element.external) {
    return (
      <a
        href={props.element.url}
        aria-label={props.element.title}
        className={finalClassName}
        target={target}
      >
        {props.element.text}
      </a>
    );
  }
  return (
    <Link
      href={props.element.url}
      className={finalClassName}
      target={target}
      title={props.element.title}
    >
      {props.element.text}
    </Link>
  );
};
