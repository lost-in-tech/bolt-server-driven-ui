import { Element, RenderElementProps } from "./sdui/Element";

type ImageElement = Element & {
  url: string;
  alt: string;
};

export const Image = (props: RenderElementProps<ImageElement>) => {
  return (
    <img
      src={props.element.url}
      alt={props.element.alt}
      style={{
        width: "100%",
      }}
    />
  );
};
