import { cookies } from "next/headers";
import { LayoutNames, Screen } from "./types";

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

const fetchSdui = async (props: FetchSduiProps): Promise<Screen> => {
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
  return await rsp.json();
};

export default fetchSdui;
