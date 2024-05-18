import { Responsive } from "./Responsive";
import { Space } from "./Space";

export type Margin = {
  margin?: Responsive<Space>;
  marginX?: Responsive<Space>;
  marginY?: Responsive<Space>;
  marginLeft?: Responsive<Space>;
  marginRight?: Responsive<Space>;
  marginTop?: Responsive<Space>;
  marginBottom?: Responsive<Space>;
};
