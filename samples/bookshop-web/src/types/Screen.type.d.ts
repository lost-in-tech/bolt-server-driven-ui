export type ResponseInstruction = {
  httpStatusCode: number;
  redirectUrl?: string;
};

export interface IElement {
  _type: string;
}

export type Layout = {
  versionId: string;
  element: IElement;
};

export type Layouts = {
  wide?: Layout;
  compact?: Layout;
};

export type ScreenSection = {
  name: string;
  element: IElement;
};

export interface IMetaData {
  _type: string;
}

export type Screen = {
  responseInstruction: ResponseInstruction;
  layouts: Layouts;
  sections: ScreenSection[];
  contextData: any;
  metaData: IMetaData[];
};
