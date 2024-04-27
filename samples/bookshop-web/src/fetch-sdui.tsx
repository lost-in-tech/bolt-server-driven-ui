import { Screen } from "./types";

const fetchSdui = async (path: string): Promise<Screen> => {
  const hasSlash = path?.indexOf("/") === 0;
  const url = `${process.env.API_URL}${hasSlash ? "" : "/"}${path}`;

  console.log("calling api", url);
  var rsp = await fetch(url, {
    cache: "no-store",
  });

  return await rsp.json();
};

export default fetchSdui;
