import { RenderElement, RenderChildElements } from "./sdui/RenderElement";
import { Padding } from "@/design-system/Padding";
import { Element, RenderElementProps } from "./sdui/Element";
import { Margin } from "@/design-system/Margin";
import { Responsive } from "@/design-system/Responsive";
import { FontSize, FontWeight } from "@/design-system/Font";
import { buildPaddingClassNames } from "@/design-system/PaddingStyle";
import { buildMarginClassNames } from "@/design-system/MarginStyle";
import { Width } from "@/design-system/Width";
import {
  buildMinMaxWidthStyle,
  buildWidthClassNames,
} from "@/design-system/WidthStyle";
import {
  buildFontSizeClassNames,
  buildFontWeightClassNames,
} from "@/design-system/FontStyle";
import {
  buildBgColorClassNames,
  buildTextColorClassNames,
} from "@/design-system/ColorStyle";
import { Color } from "@/design-system/Color";
import { Height } from "@/design-system/Height";
import {
  buildHeightClassNames,
  buildMinMaxHeightStyle,
} from "@/design-system/HeightStyle";

type BlockElement = Element &
  Padding &
  Margin & {
    minWidth?: number;
    maxWidth?: number;
    minHeight?: number;
    maxHeight?: number;
    fontSize?: Responsive<FontSize>;
    fontWeight?: Responsive<FontWeight>;
    textColor?: Responsive<Color>;
    width?: Responsive<Width>;
    height?: Responsive<Height>;
    bgColor?: Responsive<Color>;
  };

export const Block = (props: RenderElementProps<BlockElement>) => {
  const classNames: string[] = [];
  classNames.push("inline-block w-full h-full");
  classNames.push(buildPaddingClassNames(props.element));
  classNames.push(buildMarginClassNames(props.element));
  classNames.push(buildWidthClassNames(props.element.width));
  classNames.push(buildHeightClassNames(props.element.height));
  classNames.push(buildFontSizeClassNames(props.element.fontSize));
  classNames.push(buildFontWeightClassNames(props.element.fontWeight));
  classNames.push(buildBgColorClassNames(props.element.bgColor));
  classNames.push(buildTextColorClassNames(props.element.textColor));

  const style = {
    ...buildMinMaxWidthStyle(props.element.minWidth, props.element.maxWidth),
    ...buildMinMaxHeightStyle(props.element.minHeight, props.element.maxHeight),
  };

  return (
    <div style={style} className={classNames.join(" ")}>
      <RenderChildElements {...props} />
    </div>
  );
};
