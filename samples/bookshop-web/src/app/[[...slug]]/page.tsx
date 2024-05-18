import { PageMetaData } from "@/components/PageMetaData";
import { SduiProps } from "@/components/sdui/Sdui";
import { SduiApp } from "@/components/sdui/SduiApp";
import fetchSdui, { FetchSduiProps } from "@/fetch-sdui";
import { getApiRequestUrl } from "@/utils/api-request-url";
import { cookies } from "next/headers";

type Props = {
  params: { slug: string[] | undefined };
  searchParams: { [key: string]: string | string[] | undefined };
};

const buildFetchProps = ({ params, searchParams }: Props): FetchSduiProps => {
  const apiRequestUrl = getApiRequestUrl(params?.slug, searchParams);
  const isMobile = false; // TODO: figure out based on your logic
  const activeLayout = cookies().get("sdui-active-layout")?.value;
  console.log(activeLayout);
  const layout = activeLayout ? activeLayout : isMobile ? "compact" : "wide";
  console.log(apiRequestUrl);

  return {
    app: "web-bookworm",
    device: "Desktop",
    platform: "Windows",
    requestUri: apiRequestUrl,
    screenSize: "wide",
    tenant: "bookworm-au",
    url: apiRequestUrl,
  };
};

// or Dynamic metadata
export async function generateMetadata(props: Props) {
  const fetchProps = buildFetchProps(props);

  var rsp = await fetchSdui(fetchProps);

  const data = rsp.metaData?.find(
    (x) => x._type === "PageMetaData"
  ) as PageMetaData;

  if (!data) return;

  const obj: { [key: string]: any } = {};

  data.items.forEach((item) => {
    obj[item.name] = item.content;
  });

  obj["title"] = data.title;
  obj["og"] = {
    title: "testing",
  };

  return obj;
}

export default async function Page(props: Props) {
  const fetchProps = buildFetchProps(props);

  var rsp = await fetchSdui(fetchProps);

  const suidProp: SduiProps = {
    appName: fetchProps.app,
    baseUrl: process.env.API_URL ?? "",
    device: fetchProps.device,
    layout: fetchProps.screenSize,
    platform: fetchProps.platform,
    requestUrl: fetchProps.requestUri,
    screen: rsp,
    tenant: fetchProps.tenant,
  };

  return <SduiApp {...suidProp} />;
}
