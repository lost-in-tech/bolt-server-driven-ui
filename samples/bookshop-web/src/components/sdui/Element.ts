import { ScreenSection } from "@/types";

export type Element = {
  _type: string;
  elements?: Element[];
};

export type RenderElementProps<T extends Element> = {
  element: T;
  sectionsMap: Map<string, Element>;
};
