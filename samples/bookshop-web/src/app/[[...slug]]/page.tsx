import RenderElement from "@/components/RenderElement";
import fetchSdui from "@/fetch-sdui";
import { notFound } from "next/navigation";

export default async function Page({ params }: { params: { slug: string[] } }) {
  const slug = params?.slug?.join("/") ?? "/";
  var rsp = await fetchSdui(slug);

  // if (rsp.responseInstruction.httpStatusCode === 404) {
  //   notFound();
  // }

  if (rsp.layouts.wide?.element) {
    return (
      <RenderElement
        {...{ element: rsp.layouts.wide?.element, sections: rsp.sections }}
      />
    );
  }

  return <></>;
}
