import { IElement, RenderElementProps, Responsive, StackWidth } from "@/types";
import RenderElement from "./RenderElement";

import { DirectionType } from "@/types/direction.type";
import { responsiveSpacing, sprinkles } from "@/design-system/sprinkles.css";
import {
  responsiveWidthMap,
  responsiveStackWidthPercentMap,
} from "@/design-system/sprinkles.css";
import { UiSpace } from "@/types/UiSpace.type";

interface StackElement extends IElement {
  gap?: Responsive<UiSpace>;
  direction?: Responsive<DirectionType>;
  width?: Responsive<StackWidth>;
  minWidth?: Responsive<number>;
  maxWidth?: Responsive<number>;
  elements: IElement[];
}

const mapToFlexDirection = (value?: DirectionType): "column" | "row" => {
  if (value === null || value === undefined) return "column";
  return value === DirectionType.horizontal ? "row" : "column";
};

const mapToWidth = (value?: StackWidth) => {
  if (value === null || value === undefined) return "100%";

  if (value === StackWidth.full) return "100%";
  if (value === StackWidth.oneHalf) return "50%";
  if (value === StackWidth.oneThird) return "33.3333%";
  if (value === StackWidth.oneFourth) return "25%";
  if (value === StackWidth.oneFifth) return "20%";
  if (value === StackWidth.oneSixth) return "16.6666%";
  if (value === StackWidth.oneSeventh) return "14.2857%";
  if (value === StackWidth.oneEighth) return "12.5%";
  if (value === StackWidth.oneNinth) return "11.1111%";
  if (value === StackWidth.oneTenth) return "10%";

  return "100%";
};

const style = (elm: StackElement) => {
  return sprinkles({
    display: "flex",
    flexDirection: [
      mapToFlexDirection(elm.direction?.xs ?? DirectionType.vertical),
      mapToFlexDirection(elm.direction?.xs ?? DirectionType.vertical),
      mapToFlexDirection(
        elm.direction?.sm ?? elm.direction?.xs ?? DirectionType.vertical
      ),
      mapToFlexDirection(
        elm.direction?.md ??
          elm.direction?.sm ??
          elm.direction?.xs ??
          DirectionType.vertical
      ),
      mapToFlexDirection(
        elm.direction?.lg ??
          elm.direction?.md ??
          elm.direction?.sm ??
          elm.direction?.xs ??
          DirectionType.vertical
      ),
    ],
    gap: [
      responsiveSpacing[elm.gap?.xs ?? UiSpace.none],
      responsiveSpacing[elm.gap?.xs ?? UiSpace.none],
      responsiveSpacing[elm.gap?.sm ?? elm.gap?.xs ?? UiSpace.none],
      responsiveSpacing[
        elm.gap?.md ?? elm.gap?.sm ?? elm.gap?.xs ?? UiSpace.none
      ],
      responsiveSpacing[
        elm.gap?.lg ?? elm.gap?.md ?? elm.gap?.sm ?? elm.gap?.xs ?? UiSpace.none
      ],
    ],
    width: [
      responsiveStackWidthPercentMap[elm.width?.xs ?? StackWidth.full],
      responsiveStackWidthPercentMap[elm.width?.xs ?? StackWidth.full],
      responsiveStackWidthPercentMap[
        elm.width?.sm ?? elm.width?.xs ?? StackWidth.full
      ],
      responsiveStackWidthPercentMap[
        elm.width?.md ?? elm.width?.sm ?? elm.width?.xs ?? StackWidth.full
      ],
      responsiveStackWidthPercentMap[
        elm.width?.lg ??
          elm.width?.md ??
          elm.width?.sm ??
          elm.width?.xs ??
          StackWidth.full
      ],
    ],
    minWidth: [
      responsiveWidthMap[elm.minWidth?.xs ?? 0],
      responsiveWidthMap[elm.minWidth?.xs ?? 0],
      responsiveWidthMap[elm.minWidth?.sm ?? elm.minWidth?.xs ?? 0],
      responsiveWidthMap[
        elm.minWidth?.md ?? elm.minWidth?.sm ?? elm.minWidth?.xs ?? 0
      ],
      responsiveWidthMap[
        elm.minWidth?.lg ??
          elm.minWidth?.md ??
          elm.minWidth?.sm ??
          elm.minWidth?.xs ??
          0
      ],
    ],
    maxWidth: [
      responsiveWidthMap[elm.maxWidth?.xs ?? 0],
      responsiveWidthMap[elm.maxWidth?.xs ?? 0],
      responsiveWidthMap[elm.maxWidth?.sm ?? elm.maxWidth?.xs ?? 0],
      responsiveWidthMap[
        elm.maxWidth?.md ?? elm.maxWidth?.sm ?? elm.maxWidth?.xs ?? 0
      ],
      responsiveWidthMap[
        elm.maxWidth?.lg ??
          elm.maxWidth?.md ??
          elm.maxWidth?.sm ??
          elm.maxWidth?.xs ??
          0
      ],
    ],
  });
};

const Stack = (props: RenderElementProps<StackElement>) => {
  const childElements = props.element.elements;
  return (
    <div className={style(props.element)}>
      {childElements?.map((e, index) => (
        <RenderElement
          key={index}
          {...{ element: e, sections: props.sections }}
        />
      ))}
    </div>
  );
};

export default Stack;
