import { ReactNode } from "react";
import Stack from "../Stack";
import Text from "../Text";
import { Container } from "../Container";
import Placeholder from "../Placeholder";
import EmptyElement from "../EmptyElement";
import { Heading, Paragraph, Image, NavigateLink, Block, Divider } from "..";
import { Element, RenderElementProps } from "./Element";
import { LazyBlock } from "../LazyBlock";
import { SimpleLink } from "../SimpleLink";
import { Form } from "../form";
import { Input } from "../form/input";

const ElementsRegistry: Record<
  string,
  (props: RenderElementProps<Element>) => ReactNode
> = {
  Stack: (props: any) => <Stack {...props} />,
  Text: (props: any) => <Text {...props} />,
  SimpleLink: (props: any) => <SimpleLink {...props} />,
  Form: (props: any) => <Form {...props} />,
  Input: (props: any) => <Input {...props} />,
  Placeholder: (props: any) => <Placeholder {...props} />,
  Container: (props: any) => <Container {...props} />,
  Paragraph: (props: any) => <Paragraph {...props} />,
  Heading: (props: any) => <Heading {...props} />,
  Image: (props: any) => <Image {...props} />,
  NavigateLink: (props: any) => <NavigateLink {...props} />,
  Block: (props: any) => <Block {...props} />,
  Divider: (props: any) => <Divider {...props} />,
  LazyBlock: (props: any) => <LazyBlock {...props} />,
  EmptyElement: (props: any) => <EmptyElement {...props} />,
};

export default ElementsRegistry;
