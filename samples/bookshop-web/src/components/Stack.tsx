import { Responsive } from "@/design-system/Responsive";
import { RenderChildElements } from "./sdui/RenderElement";

import { Element, RenderElementProps } from "./sdui/Element";
import { Padding } from "@/design-system/Padding";
import { Margin } from "@/design-system/Margin";
import { Border } from "@/design-system/Border";
import { Space } from "@/design-system/Space";
import { Width } from "@/design-system/Width";
import { buildPaddingClassNames } from "@/design-system/PaddingStyle";
import { buildMarginClassNames } from "@/design-system/MarginStyle";
import { buildGapClassNames } from "@/design-system/GapStyle";
import {
  buildMinMaxWidthStyle,
  buildWidthClassNames,
} from "@/design-system/WidthStyle";
import { buildFlexDirectionClassNames } from "@/design-system/DirectionStyle";
import { Direction } from "@/design-system/Direction";
import { ContentAlign } from "@/design-system/ContentAlign";
import { buildContentAlignClassNames } from "@/design-system/ContentAlignStyle";
import {
  buildHeightClassNames,
  buildMinMaxHeightStyle,
} from "@/design-system/HeightStyle";
import { Height } from "@/design-system/Height";
import { Color } from "@/design-system/Color";
import { FontSize, FontWeight } from "@/design-system/Font";
import {
  buildFontSizeClassNames,
  buildFontWeightClassNames,
} from "@/design-system/FontStyle";
import {
  buildBgColorClassNames,
  buildTextColorClassNames,
} from "@/design-system/ColorStyle";

type StackElement = Element &
  Padding &
  Margin &
  Border &
  ContentAlign & {
    gap?: Responsive<Space>;
    direction?: Responsive<Direction>;
    width?: Responsive<Width>;
    height?: Responsive<Height>;
    minWidth?: number;
    maxWidth?: number;
    minHeight?: number;
    maxHeight?: number;

    textColor?: Responsive<Color>;
    fontSize?: Responsive<FontSize>;
    fontWeight?: Responsive<FontWeight>;
    bgColor?: Responsive<Color>;
  };

const Stack = (props: RenderElementProps<StackElement>) => {
  const classNames: string[] = [];
  classNames.push("flex w-full h-full");
  classNames.push(buildPaddingClassNames(props.element));
  classNames.push(buildMarginClassNames(props.element));
  classNames.push(buildGapClassNames(props.element.gap));
  classNames.push(buildWidthClassNames(props.element.width));
  classNames.push(buildHeightClassNames(props.element.height));
  classNames.push(buildContentAlignClassNames(props.element));
  classNames.push(buildFontSizeClassNames(props.element.fontSize));
  classNames.push(buildFontWeightClassNames(props.element.fontWeight));
  classNames.push(buildBgColorClassNames(props.element.bgColor));
  classNames.push(buildTextColorClassNames(props.element.textColor));
  classNames.push(
    buildFlexDirectionClassNames(props.element.direction, "flex-col")
  );

  const style = {
    ...buildMinMaxWidthStyle(props.element.minWidth, props.element.maxWidth),
    ...buildMinMaxHeightStyle(props.element.minHeight, props.element.maxHeight),
  };

  return (
    <div className={classNames.join(" ")} style={style}>
      <RenderChildElements {...props} />
    </div>
  );
};

export default Stack;
