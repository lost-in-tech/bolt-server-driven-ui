"use client";
import { IElement, RenderElementProps } from "@/types";
import RenderElement from "./RenderElement";
import Link from "next/link";
import { sprinkles } from "@/design-system/sprinkles.css";
import { publish, PublishEventProps } from "@/utils/csutom-events";

interface LinkElement extends IElement {
  url: string;
  elements: IElement[];
  onClick?: PublishEventProps;
}

export const NavigateLink = (props: RenderElementProps<LinkElement>) => {
  const childElements = props.element.elements;

  const handleClick = (
    event: React.MouseEvent<HTMLAnchorElement, MouseEvent>
  ): void => {
    if (props.element.onClick) {
      event.preventDefault();
      publish(props.element.onClick);
    }
  };

  return (
    <Link
      className={sprinkles({
        color: "base-content",
      })}
      href={props.element.url}
      onClick={handleClick}
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
