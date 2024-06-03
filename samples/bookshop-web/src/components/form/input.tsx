import { Element, RenderElementProps } from "../sdui/Element";

export type InputProps = Element & {
  name: string;
};

export const Input = (props: RenderElementProps<InputProps>) => {
  return <input name={props.element.name} type="text" value="" />;
};
