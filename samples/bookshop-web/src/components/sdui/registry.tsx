import { ReactNode } from "react";
import Stack from "../Stack";
import Text from "../Text";
import { Container } from "../Container";
import Placeholder from "../Placeholder";
import EmptyElement from "../EmptyElement";
import { IElement, RenderElementProps } from "@/types";
import { Heading, Paragraph, Image, NavigateLink } from "..";

const ComponentRegistry: Record<
  string,
  (props: RenderElementProps<IElement>) => ReactNode
> = {
  Stack: (props: any) => <Stack {...props} />,
  Text: (props: any) => <Text {...props} />,
  Placeholder: (props: any) => <Placeholder {...props} />,
  Container: (props: any) => <Container {...props} />,
  Paragraph: (props: any) => <Paragraph {...props} />,
  Heading: (props: any) => <Heading {...props} />,
  Image: (props: any) => <Image {...props} />,
  NavigateLink: (props: any) => <NavigateLink {...props} />,
  EmptyElement: (props: any) => <EmptyElement {...props} />,
};

export default ComponentRegistry;
