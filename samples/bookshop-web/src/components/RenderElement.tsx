import ComponentRegistry from "@/components/sdui/registry";
import { IElement, RenderElementProps } from "@/types";

const RenderElement = <T extends IElement>(props: RenderElementProps<T>) => {
  var cmp = ComponentRegistry[props.element?._type];
  if (cmp) return cmp(props);
  return null;
};

export default RenderElement;
