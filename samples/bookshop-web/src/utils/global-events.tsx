"use client";
import { RedirectType, redirect, useRouter } from "next/navigation";
import { subscribe, unsubscribe } from "./csutom-events";
import { useEffect } from "react";

export enum EventNames {
  navigate = "navigate",
}

export const GlobalEvents = () => {
  const router = useRouter();

  const onNavigate = (e: any) => {
    if (e.detail.isExternal) {
      document.location = e.detail.url;
    } else {
      router.push(e.detail.url);
    }
  };

  useEffect(() => {
    subscribe(EventNames.navigate, onNavigate);

    return () => {
      unsubscribe(EventNames.navigate, onNavigate);
    };
  }, []);

  return <></>;
};
