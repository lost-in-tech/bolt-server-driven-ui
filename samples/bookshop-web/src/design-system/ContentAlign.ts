import { Responsive } from "./Responsive";

export type ContentAlignX = "left" | "right" | "center";
export type ContentAlignY = "top" | "bottom" | "center";
export type ContentAlign = {
  contentAlignX?: Responsive<ContentAlignX>;
  contentAlignY?: Responsive<ContentAlignY>;
};
