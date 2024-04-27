import { IElement, RenderElementProps } from "@/types";

interface ImageElement extends IElement {
  url: string;
  alt: string;
}

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
