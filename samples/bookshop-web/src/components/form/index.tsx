import { publish } from "@/utils/csutom-events";
import React, { ReactNode } from "react";
import { SduiEventGroups, SduiEvents } from "../sdui/SduiEvents";

export type FormProps = {
  submitUrl: string;
  children: ReactNode;
};
export const Form = (props: FormProps) => {
  const handleFormSubmit = (e: any) => {
    e.preventDefault();
    const data = new FormData(e.target);
    console.log(data);
    publish({
      name: SduiEvents.formSubmitRequested,
      group: SduiEventGroups.sdui,
      data: data,
    });
  };

  return (
    <form method="post" action={props.submitUrl} onSubmit={handleFormSubmit}>
      {props.children}
    </form>
  );
};
