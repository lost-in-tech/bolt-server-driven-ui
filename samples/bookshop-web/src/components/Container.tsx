import { IElement, RenderElementProps, Responsive } from "@/types";
import RenderElement from "./RenderElement";
import { sprinkles, responsiveSpacing } from "@/design-system/sprinkles.css";
import { UiSpace } from "@/types/UiSpace.type";

interface ContainerElement extends IElement {
  padding: Responsive<UiSpace>;
  elements: IElement[];
}

const style = (props: Responsive<UiSpace>) => {
  props ??= {
    xs: UiSpace.md,
  } as Responsive<UiSpace>;
  return sprinkles({
    maxWidth: ["468px", "640px", "768px", "1024px"],
    paddingLeft: props
      ? [
          responsiveSpacing[props?.xs ?? UiSpace.none],
          responsiveSpacing[props?.xs ?? UiSpace.none],
          responsiveSpacing[props?.sm ?? props?.xs ?? UiSpace.none],
          responsiveSpacing[
            props?.md ?? props?.sm ?? props?.xs ?? UiSpace.none
          ],
          responsiveSpacing[
            props?.lg ?? props?.md ?? props?.sm ?? props?.xs ?? UiSpace.none
          ],
        ]
      : [responsiveSpacing[UiSpace.none]],
    paddingRight: props
      ? [
          responsiveSpacing[props?.xs ?? UiSpace.none],
          responsiveSpacing[props?.xs ?? UiSpace.none],
          responsiveSpacing[props?.sm ?? props?.xs ?? UiSpace.none],
          responsiveSpacing[
            props?.md ?? props?.sm ?? props?.xs ?? UiSpace.none
          ],
          responsiveSpacing[
            props?.lg ?? props?.md ?? props?.sm ?? props?.xs ?? UiSpace.none
          ],
        ]
      : [responsiveSpacing[UiSpace.none]],
  });
};

export const Container = (props: RenderElementProps<ContainerElement>) => {
  const childElements = props.element.elements;
  return (
    <div
      className={style(props.element.padding)}
      style={{ margin: "0 auto", width: "100%" }}
    >
      {childElements?.map((e, index) => (
        <RenderElement
          key={index}
          {...{ element: e, sections: props.sections }}
        />
      ))}
    </div>
  );
};
