import { globalStyle } from "@vanilla-extract/css";
import { vars } from "@/design-system/theme.css";

globalStyle("html", {
  height: "100%",
});

globalStyle("*", {
  boxSizing: "border-box",
  border: 0,
  padding: 0,
  margin: 0,
});

globalStyle("body", {
  height: "100%",
  fontFamily: vars.fontNames.default,
  padding: 0,
  margin: 0,
  border: 0,
  color: vars.colors.baseContent,
  fontSize: vars.fontSizes.md,
  fontWeight: vars.fontWeight.normal,
});
