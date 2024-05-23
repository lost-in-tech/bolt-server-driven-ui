import { buildPaddingClassNames } from "@/design-system/PaddingStyle";
import { Element, RenderElementProps } from "./sdui/Element";
import { RenderElement, RenderChildElements } from "./sdui/RenderElement";
import { Padding } from "@/design-system/Padding";

type ContainerElement = Element & Padding & {};

export const Container = (props: RenderElementProps<ContainerElement>) => {
  const classNames: string[] = [];

  classNames.push("px-4 md:px-5 max-w-5xl h-full w-full");
  classNames.push(buildPaddingClassNames(props.element));

  return (
    <div
      className={classNames.join(" ")}
      style={{ margin: "0 auto", width: "100%" }}
    >
      <RenderChildElements {...props}></RenderChildElements>
    </div>
  );
};
