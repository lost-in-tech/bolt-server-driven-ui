import { IElement, RenderElementProps, Responsive } from "@/types";
import RenderElement from "./RenderElement";
import { UiSpace } from "@/types/UiSpace.type";
import { responsiveSpacing, sprinkles } from "@/design-system/sprinkles.css";

interface BlockElement extends IElement {
  minWidth: number;
  maxWidth: number;

  paddingLeft?: Responsive<UiSpace>;
  paddingRight?: Responsive<UiSpace>;
  paddingTop?: Responsive<UiSpace>;
  paddingBottom?: Responsive<UiSpace>;

  elements: IElement[];
}

export const Block = (props: RenderElementProps<BlockElement>) => {
  const childElements = props.element.elements;
  const needClass =
    props.element.paddingLeft ||
    props.element.paddingRight ||
    props.element.paddingTop ||
    props.element.paddingBottom ||
    props.element.minWidth ||
    props.element.maxWidth;

  return (
    <div
      className={
        needClass
          ? sprinkles({
              paddingTop: [
                responsiveSpacing[props.element.paddingTop?.xs ?? UiSpace.none],
                responsiveSpacing[props.element.paddingTop?.xs ?? UiSpace.none],
                responsiveSpacing[
                  props.element.paddingTop?.sm ??
                    props.element.paddingTop?.xs ??
                    UiSpace.none
                ],
                responsiveSpacing[
                  props.element.paddingTop?.md ??
                    props.element.paddingTop?.sm ??
                    props.element.paddingTop?.xs ??
                    UiSpace.none
                ],
                responsiveSpacing[
                  props.element.paddingTop?.lg ??
                    props.element.paddingTop?.md ??
                    props.element.paddingTop?.sm ??
                    props.element.paddingTop?.xs ??
                    UiSpace.none
                ],
              ],

              paddingBottom: [
                responsiveSpacing[
                  props.element.paddingBottom?.xs ?? UiSpace.none
                ],
                responsiveSpacing[
                  props.element.paddingBottom?.xs ?? UiSpace.none
                ],
                responsiveSpacing[
                  props.element.paddingBottom?.sm ??
                    props.element.paddingBottom?.xs ??
                    UiSpace.none
                ],
                responsiveSpacing[
                  props.element.paddingBottom?.md ??
                    props.element.paddingBottom?.sm ??
                    props.element.paddingBottom?.xs ??
                    UiSpace.none
                ],
                responsiveSpacing[
                  props.element.paddingBottom?.lg ??
                    props.element.paddingBottom?.md ??
                    props.element.paddingBottom?.sm ??
                    props.element.paddingBottom?.xs ??
                    UiSpace.none
                ],
              ],

              paddingLeft: [
                responsiveSpacing[
                  props.element.paddingLeft?.xs ?? UiSpace.none
                ],
                responsiveSpacing[
                  props.element.paddingLeft?.xs ?? UiSpace.none
                ],
                responsiveSpacing[
                  props.element.paddingLeft?.sm ??
                    props.element.paddingLeft?.xs ??
                    UiSpace.none
                ],
                responsiveSpacing[
                  props.element.paddingLeft?.md ??
                    props.element.paddingLeft?.sm ??
                    props.element.paddingLeft?.xs ??
                    UiSpace.none
                ],
                responsiveSpacing[
                  props.element.paddingLeft?.lg ??
                    props.element.paddingLeft?.md ??
                    props.element.paddingLeft?.sm ??
                    props.element.paddingLeft?.xs ??
                    UiSpace.none
                ],
              ],

              paddingRight: [
                responsiveSpacing[
                  props.element.paddingRight?.xs ?? UiSpace.none
                ],
                responsiveSpacing[
                  props.element.paddingRight?.xs ?? UiSpace.none
                ],
                responsiveSpacing[
                  props.element.paddingRight?.sm ??
                    props.element.paddingRight?.xs ??
                    UiSpace.none
                ],
                responsiveSpacing[
                  props.element.paddingRight?.md ??
                    props.element.paddingRight?.sm ??
                    props.element.paddingRight?.xs ??
                    UiSpace.none
                ],
                responsiveSpacing[
                  props.element.paddingRight?.lg ??
                    props.element.paddingRight?.md ??
                    props.element.paddingRight?.sm ??
                    props.element.paddingRight?.xs ??
                    UiSpace.none
                ],
              ],
            })
          : undefined
      }
      style={{
        display: "flex",
        width: "100%",
        minWidth: props.element.minWidth
          ? `${props.element.minWidth}px`
          : undefined,
        maxWidth: props.element.maxWidth
          ? `${props.element.maxWidth}px`
          : undefined,
      }}
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
