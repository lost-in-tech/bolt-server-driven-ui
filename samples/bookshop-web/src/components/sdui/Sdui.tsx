import { LayoutNames, Layouts, Screen } from "@/types";
import { GlobalEvents } from "@/utils/global-events";
import RenderElement from "../RenderElement";

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
    console.log(props.layout);
    console.log(props.screen.layouts);
    return <></>;
  }

  console.log("sdui...");

  return (
    <>
      <GlobalEvents />
      <RenderElement
        {...{
          element: element,
          sections: props.screen.sections,
        }}
      />
    </>
  );
};
