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
import { buildWidthClassNames } from "@/design-system/WidthStyle";
import { buildFlexDirectionClassNames } from "@/design-system/DirectionStyle";
import { Direction } from "@/design-system/Direction";
import { ContentAlign } from "@/design-system/ContentAlign";
import { buildContentAlignClassNames } from "@/design-system/ContentAlignStyle";

type StackElement = Element &
  Padding &
  Margin &
  Border &
  ContentAlign & {
    gap?: Responsive<Space>;
    direction?: Responsive<Direction>;
    width?: Responsive<Width>;
    minWidth?: Responsive<number>;
    maxWidth?: Responsive<number>;
  };

const Stack = (props: RenderElementProps<StackElement>) => {
  const classNames: string[] = [];
  classNames.push("flex");
  classNames.push(buildPaddingClassNames(props.element));
  classNames.push(buildMarginClassNames(props.element));
  classNames.push(buildGapClassNames(props.element.gap));
  classNames.push(buildWidthClassNames(props.element.width));
  classNames.push(buildContentAlignClassNames(props.element));
  classNames.push(
    buildFlexDirectionClassNames(props.element.direction, "flex-col")
  );

  return (
    <div className={classNames.join(" ")}>
      <RenderChildElements {...props} />
    </div>
  );
};

export default Stack;
