import { Element, RenderElementProps } from "./sdui/Element";

type ParagraphElement = Element & {
  text: string;
};

export const Paragraph = (props: RenderElementProps<ParagraphElement>) => {
  return <p style={{ lineHeight: "26px" }}>{props.element.text}</p>;
};
