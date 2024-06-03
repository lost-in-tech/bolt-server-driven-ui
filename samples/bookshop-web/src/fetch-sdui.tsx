import { cookies } from "next/headers";
import { LayoutNames, Screen } from "./types";
import { Element } from "./components/sdui/Element";

export type FetchSduiProps = {
  url: string;
  screenSize: LayoutNames;
  app: string;
  tenant: string;
  requestUri: string;
  device: string;
  platform: string;
  cookies?: string;
};

export type FetchSduiRsp = {
  screen: Screen;
  sectionsMap: Map<string, Element>;
};

const fetchSdui = async (props: FetchSduiProps): Promise<FetchSduiRsp> => {
  const rsp = await fetch(props.url, {
    headers: {
      "x-screen": props.screenSize,
      "x-tenant": props.tenant,
      "x-request-uri": props.requestUri,
      "x-device": props.device,
      "x-platform": props.platform,
      "x-app": props.app,
      Cookie: props.cookies ?? "",
    },
    cache: "no-cache",
    credentials: "include",
  });
  var screen = (await rsp.json()) as Screen;

  return {
    screen: screen,
    sectionsMap: new Map<string, Element>(
      screen.sections.map((s) => [s.name, s.element])
    ),
  } as FetchSduiRsp;
};

export default fetchSdui;
