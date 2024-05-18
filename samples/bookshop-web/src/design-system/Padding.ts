import { Responsive } from "./Responsive";
import { Space } from "./Space";

export type Padding = {
  padding?: Responsive<Space>;
  paddingX?: Responsive<Space>;
  paddingY?: Responsive<Space>;
  paddingLeft?: Responsive<Space>;
  paddingRight?: Responsive<Space>;
  paddingTop?: Responsive<Space>;
  paddingBottom?: Responsive<Space>;
};
