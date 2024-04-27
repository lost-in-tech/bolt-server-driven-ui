import { IMetaData } from "@/types";

export type PageMetaDataItem = {
  content: string;
  name: string;
};

export interface PageMetaData extends IMetaData {
  title: string;
  metaData: PageMetaDataItem[];
}
