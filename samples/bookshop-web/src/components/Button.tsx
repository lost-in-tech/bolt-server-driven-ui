import { Responsive } from "@/design-system/Responsive";
import { Element, RenderElementProps } from "./sdui/Element";
import { Color } from "@/design-system/Color";
import { Icon, IconNames } from "./icons";

type ButtonSize = "xs" | "sm" | "md" | "lg" | "xl";
type ButtonVariant = "solid" | "soft" | "ghost" | "outline";
type ButtonIntent =
  | "primary"
  | "secondary"
  | "accent"
  | "error"
  | "success"
  | "information"
  | "warning";

type ButtonType = "submit" | "reset" | "button";

type ButtonProps = Element & {
  textColor?: Responsive<Color>;
  bgColor?: Responsive<Color>;
  size?: Responsive<ButtonSize>;
  variant?: ButtonVariant;
  intent?: ButtonIntent;
  disabled?: boolean;
  text?: string;
  description?: string;
  preIcon?: IconNames;
  postIcon?: IconNames;
  buttonType?: ButtonType;
};

export const Button = (props: RenderElementProps<ButtonProps>) => {
  return (
    <button
      type={props.element.buttonType}
      aria-description={props.element.description ?? props.element.text}
    >
      <Icon name={props.element.preIcon} />
      {props.element.text}
      <Icon name={props.element.postIcon} />
    </button>
  );
};
