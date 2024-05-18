import { Color } from "./Color";
import { Responsive } from "./Responsive";

export type BorderWidth =
  | "none"
  | "regular"
  | "medium"
  | "thick"
  | "extraThick";

export type Border = {
  borderColor?: Responsive<Color>;
  borderWidth: Responsive<BorderWidth>;
  borderWidthX?: Responsive<BorderWidth>;
  borderWidthY?: Responsive<BorderWidth>;
  borderWidthTop?: Responsive<BorderWidth>;
  borderWidthBottom?: Responsive<BorderWidth>;
  borderWidthLeft?: Responsive<BorderWidth>;
  borderWidthRight?: Responsive<BorderWidth>;
};
