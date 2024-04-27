import { Screen } from "./types";

const fetchSdui = async (path: string): Promise<Screen> => {
  const hasSlash = path?.indexOf("/") === 0;
  const url = `${process.env.API_URL}${hasSlash ? "" : "/"}${path}`;

  var rsp = await fetch(url);

  return await rsp.json();
};

export default fetchSdui;
