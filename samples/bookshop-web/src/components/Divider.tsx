import { Color } from "@/design-system/Color";
import { Element, RenderElementProps } from "./sdui/Element";

type DividerElement = Element & {
  color?: Color;
};

export const Divider = (props: RenderElementProps<DividerElement>) => {
  return (
    <div
      className="bg-base-content"
      style={{
        width: "100%",
        minHeight: "3px",
        maxHeight: "3px",
      }}
    ></div>
  );
};
