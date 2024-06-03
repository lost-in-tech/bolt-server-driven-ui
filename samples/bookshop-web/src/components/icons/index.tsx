import { ReactNode } from "react";
import { IconCheck } from "./icon-check";
import { IconChevronLeft } from "./icon-chevron-left";

export type IconNames =
  | "Check"
  | "ChevronLeft"
  | "ChevronRight"
  | "Alert"
  | "ExternalSource"
  | "Info";

const IconMaps: Record<IconNames, () => ReactNode> = {
  Check: () => <IconCheck />,
  ChevronLeft: () => <IconChevronLeft />,
  ChevronRight: () => <IconChevronLeft />,
  Alert: () => <IconChevronLeft />,
  ExternalSource: () => <IconChevronLeft />,
  Info: () => <IconChevronLeft />,
};

export type IconProps = {
  name?: IconNames;
};

export const Icon = (props: IconProps): ReactNode => {
  if (!props.name) return null;
  const elm = IconMaps[props.name];
  if (elm) return elm();
  return null;
};
