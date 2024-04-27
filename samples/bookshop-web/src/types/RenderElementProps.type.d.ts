import { IElement } from ".";
export interface RenderElementProps<T extends IElement> {
  element: T;
  sections: ScreenSection[];
}
