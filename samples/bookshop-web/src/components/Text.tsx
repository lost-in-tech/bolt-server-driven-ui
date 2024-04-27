import { IElement, RenderElementProps } from "@/types";

interface TextProps extends IElement {
  value: string;
}

const Text = (props: RenderElementProps<TextProps>) => {
  return <>{props.element.value}</>;
};

export default Text;
