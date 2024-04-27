import { IElement, RenderElementProps } from "@/types";
import RenderElement from "./RenderElement";

interface PlaceholderElement extends IElement {
  name: string;
  sections: string[];
}

const Placeholder = (props: RenderElementProps<PlaceholderElement>) => {
  if (props.element.sections == null || props.element.sections.length == 0)
    return null;

  var elements = props.element.sections.map((sectionName) => {
    const section = props.sections.find((s) => s.name == sectionName);
    return section?.element;
  });

  return (
    <>
      {elements?.map((elm, index) => {
        if (elm == null) return null;

        return (
          <RenderElement
            key={index}
            {...{ element: elm, sections: props.sections }}
          />
        );
      })}
    </>
  );
};

export default Placeholder;
