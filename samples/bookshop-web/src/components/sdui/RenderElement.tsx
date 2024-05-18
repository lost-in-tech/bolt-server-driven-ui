import ComponentRegistry from "@/components/sdui/registry";
import { Element, RenderElementProps } from "./Element";

export const RenderElement = <T extends Element>(
  props: RenderElementProps<T>
) => {
  var cmp = ComponentRegistry[props.element?._type];
  if (cmp) return cmp(props);
  return null;
};

export const RenderChildElements = <T extends Element>(
  props: RenderElementProps<T>
) => {
  const childElements = props.element.elements;

  if (childElements == null) return null;

  return (
    <>
      {childElements?.map((e, index) => (
        <RenderElement
          key={index}
          {...{ element: e, sections: props.sections }}
        />
      ))}
    </>
  );
};

export default RenderElement;
