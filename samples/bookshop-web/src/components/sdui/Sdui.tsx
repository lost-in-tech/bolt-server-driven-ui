import { LayoutNames, Layouts, Screen } from "@/types";
import { GlobalEvents } from "@/utils/global-events";
import RenderElement from "./RenderElement";
import { Element } from "./Element";

export type SduiProps = {
  appName: string;
  device: string;
  platform: string;
  tenant: string;
  screen: Screen;
  sectionsMap: Map<string, Element>;
  layout: LayoutNames;
  baseUrl: string;
  requestUrl: string;
};
export const Sdui = (props: SduiProps) => {
  const element = props.screen.layouts[props.layout]?.element;
  if (!element) {
    console.warn("No layout found");
    return <></>;
  }

  return (
    <>
      <GlobalEvents />
      <RenderElement element={element} sectionsMap={props.sectionsMap} />
    </>
  );
};
