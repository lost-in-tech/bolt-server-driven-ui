import { LayoutNames, Layouts, Screen } from "@/types";
import { GlobalEvents } from "@/utils/global-events";
import RenderElement from "./RenderElement";
import { LoadLazySections } from "./LoadLazySections";

export type SduiProps = {
  appName: string;
  device: string;
  platform: string;
  tenant: string;
  screen: Screen;
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
      <RenderElement element={element} sections={props.screen.sections} />
    </>
  );
};
