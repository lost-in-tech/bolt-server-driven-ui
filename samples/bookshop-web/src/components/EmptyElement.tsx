import { Element, RenderElementProps } from "./sdui/Element";

type EmptyElement = Element & {};

const EmptyElement = (props: RenderElementProps<EmptyElement>) => {
  return null;
};

export default EmptyElement;
