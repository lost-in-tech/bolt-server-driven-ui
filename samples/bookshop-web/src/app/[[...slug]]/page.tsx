import { PageMetaData, PageMetaDataItem } from "@/components/PageMetaData";
import RenderElement from "@/components/RenderElement";
import fetchSdui from "@/fetch-sdui";

// or Dynamic metadata
export async function generateMetadata({ params }: any) {
  const slug = params?.slug?.join("/") ?? "/";
  const rsp = await fetchSdui(slug);

  const data = rsp.metaData?.find(
    (x) => x._type === "PageMetaData"
  ) as PageMetaData;

  if (!data) return;

  // return {
  //   title: data.title,
  //   description: "testing",
  // };

  const obj: { [key: string]: string } = {};

  data.metaData.forEach((item) => {
    obj[item.name] = item.content;
  });

  obj["title"] = data.title;

  return obj;
}

export default async function Page({ params }: { params: { slug: string[] } }) {
  const slug = params?.slug?.join("/") ?? "/";
  var rsp = await fetchSdui(slug);

  // if (rsp.responseInstruction.httpStatusCode === 404) {
  //   notFound();
  // }

  if (rsp.layouts.wide?.element) {
    return (
      <>
        <RenderElement
          {...{ element: rsp.layouts.wide?.element, sections: rsp.sections }}
        />
      </>
    );
  }

  return <></>;
}
