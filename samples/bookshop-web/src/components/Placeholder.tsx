import { RenderElementProps } from "./sdui/Element";
import { RenderElement, RenderChildElements } from "./sdui/RenderElement";
import { Element } from "./sdui/Element";

type PlaceholderElement = Element & {
  name: string;
  sections: string[];
};

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
