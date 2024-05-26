"use client";

import { publish } from "@/utils/csutom-events";
import { useEffect, useState } from "react";
import { SduiEventGroups, SduiEvents } from "./SduiEvents";
import { SduiProps } from "./Sdui";
import fetchSdui from "@/fetch-sdui";
import { LayoutNames } from "@/types";

type LoadLazySectionsProps = SduiProps & {
  layout: LayoutNames;
};

export const LoadLazySections = (props: LoadLazySectionsProps) => {
  const fetchUrl = props.screen.contextData.lazyRequestPathAndQuery;

  useEffect(() => {
    publish({
      name: SduiEvents.lazySectionsLoading,
      data: {
        sections: props.screen.contextData.lazySections,
      },
      group: SduiEventGroups.sdui,
    });

    if (fetchUrl) {
      console.log(document.cookie);
      fetchSdui({
        app: props.appName,
        device: props.device,
        platform: props.platform,
        requestUri: props.requestUrl,
        screenSize: props.layout,
        tenant: props.tenant,
        url: fetchUrl,
        cookies: document.cookie,
      }).then((s) => {
        publish({
          name: SduiEvents.lazySectionsLoaded,
          data: {
            sectionsRequested: props.screen.contextData.lazySections,
            screen: s,
          },
          group: SduiEventGroups.sdui,
        });
      });
    }
  }, [props]);

  return null;
};
