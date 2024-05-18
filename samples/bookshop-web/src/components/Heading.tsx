import { FontSize } from "@/design-system/Font";
import { Element, RenderElementProps } from "./sdui/Element";
import { Responsive } from "@/design-system/Responsive";

type HeadingElement = Element & {
  text: string;
  fontSize: Responsive<FontSize>;
};

export const Heading = (props: RenderElementProps<HeadingElement>) => {
  return <h1 style={{ fontWeight: 600 }}>{props.element.text}</h1>;
};
