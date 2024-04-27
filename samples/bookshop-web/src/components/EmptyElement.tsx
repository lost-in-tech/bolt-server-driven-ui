import { IElement, RenderElementProps } from "@/types";

interface IEmptyElement extends IElement {}

const EmptyElement = (props: RenderElementProps<IEmptyElement>) => {
  return null;
};

export default EmptyElement;
