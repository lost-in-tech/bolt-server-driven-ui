"use client";
import { useCallback, useEffect, useState } from "react";
import { Sdui, SduiProps } from "./Sdui";
import { LayoutNames } from "@/types";
import fetchSdui, { FetchSduiProps } from "@/fetch-sdui";
import { LoadLazySections } from "./LoadLazySections";

const setCookie = (cname: string, cvalue: string, exdays: number) => {
  const d = new Date();
  d.setTime(d.getTime() + exdays * 24 * 60 * 60 * 1000);
  let expires = "expires=" + d.toUTCString();
  document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
};

export type SduiAppProps = SduiProps & {};

export const SduiApp = (props: SduiAppProps) => {
  const [layoutName, setLayoutName] = useState<LayoutNames>(props.layout);
  const [vm, setVm] = useState<SduiProps>(props);

  const handleWindowResized = useCallback(() => {
    setLayoutName((prev: LayoutNames): LayoutNames => {
      var layout = window.innerWidth > 640 ? "wide" : "compact";
      if (layout !== prev) {
        return layout as LayoutNames;
      }
      return prev;
    });
  }, []);

  useEffect(() => {
    handleWindowResized();
  }, []);

  useEffect(() => {
    if (layoutName === vm.layout) return;

    setCookie("sdui-active-layout", layoutName, 1);

    fetchSdui({
      url: vm.requestUrl,
      screenSize: layoutName,
      app: vm.appName,
      device: vm.device,
      platform: vm.platform,
      requestUri: vm.requestUrl,
      tenant: vm.tenant,
    }).then((rsp) => {
      setVm((prev) => {
        return {
          ...prev,
          screen: rsp,
          layout: layoutName,
        };
      });
    });
  }, [layoutName]);

  useEffect(() => {
    window.addEventListener("resize", handleWindowResized);

    return () => {
      window.removeEventListener("resize", handleWindowResized);
    };
  }, []);

  return (
    <>
      <Sdui {...vm} />
      {vm?.screen && <LoadLazySections {...props} layout={layoutName} />}
    </>
  );
};
