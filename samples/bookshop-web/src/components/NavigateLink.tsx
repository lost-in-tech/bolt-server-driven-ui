"use client";
import { RenderChildElements } from "./sdui/RenderElement";
import Link from "next/link";
import { publish, PublishEventProps } from "@/utils/csutom-events";
import { Element, RenderElementProps } from "./sdui/Element";

type LinkElement = Element & {
  url: string;
  onClick?: PublishEventProps;
};

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
    <Link href={props.element.url} onClick={handleClick}>
      <RenderChildElements {...props} />
    </Link>
  );
};
