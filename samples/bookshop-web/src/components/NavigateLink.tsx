import { IElement, RenderElementProps } from "@/types";
import RenderElement from "./RenderElement";
import Link from "next/link";
import { sprinkles } from "@/design-system/sprinkles.css";

interface LinkElement extends IElement {
  url: string;
  elements: IElement[];
}

export const NavigateLink = (props: RenderElementProps<LinkElement>) => {
  const childElements = props.element.elements;
  return (
    <Link
      className={sprinkles({
        color: "base-content",
      })}
      href={props.element.url}
    >
      {childElements?.map((e, index) => (
        <RenderElement
          key={index}
          {...{ element: e, sections: props.sections }}
        />
      ))}
    </Link>
  );
};
