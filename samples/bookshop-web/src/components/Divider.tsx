import { sprinkles } from "@/design-system/sprinkles.css";
import { IElement, RenderElementProps, Responsive } from "@/types";

interface DividerElement extends IElement {}

export const Divider = (props: RenderElementProps<DividerElement>) => {
  return (
    <div
      className={sprinkles({
        background: "base-content",
      })}
      style={{
        width: "100%",
        minHeight: "3px",
        maxHeight: "3px",
      }}
    ></div>
  );
};
