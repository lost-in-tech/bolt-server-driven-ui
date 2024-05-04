export const getApiRequestUrl = (
  slug: string[] | undefined,
  searchParams: { [key: string]: string | string[] | undefined }
) => {
  const slugStr = slug?.join("/") ?? "";
  const qs = searchParams
    ? Object.keys(searchParams)
        .map(
          (key) => `${key}=${encodeURIComponent(searchParams[key] as string)}`
        )
        .join("&")
    : null;

  return `${process.env.API_URL}/${slugStr}?${qs}`;
};
