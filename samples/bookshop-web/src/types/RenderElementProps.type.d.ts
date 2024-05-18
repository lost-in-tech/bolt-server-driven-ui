import { IElement } from ".";
export interface RenderElementProps<T extends Element> {
  element: T;
  sections: ScreenSection[];
}
