import { responsiveFontSize, sprinkles } from "@/design-system/sprinkles.css";
import { IElement, RenderElementProps, Responsive } from "@/types";
import { FontSize } from "@/types/fontSize.type";

interface HeadingElement extends IElement {
  text: string;
  fontSize: Responsive<FontSize>;
}

export const Heading = (props: RenderElementProps<HeadingElement>) => {
  return (
    <h1
      className={sprinkles({
        color: "base-content",
        fontSize: [
          responsiveFontSize[props.element.fontSize?.xs ?? FontSize.md],
          responsiveFontSize[props.element.fontSize?.xs ?? FontSize.md],
          responsiveFontSize[
            props.element.fontSize?.sm ??
              props.element.fontSize?.xs ??
              FontSize.md
          ],
          responsiveFontSize[
            props.element.fontSize?.md ??
              props.element.fontSize?.sm ??
              props.element.fontSize?.xs ??
              FontSize.md
          ],
          responsiveFontSize[
            props.element.fontSize?.lg ??
              props.element.fontSize?.md ??
              props.element.fontSize?.sm ??
              props.element.fontSize?.xs ??
              FontSize.md
          ],
        ],
      })}
      style={{ fontWeight: 600 }}
    >
      {props.element.text}
    </h1>
  );
};
