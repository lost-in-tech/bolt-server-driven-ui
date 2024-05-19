"use client";
import { useEffect, useState } from "react";
import { Element, RenderElementProps } from "./sdui/Element";
import RenderElement from "./sdui/RenderElement";
import { subscribe, unsubscribe } from "@/utils/csutom-events";
import { SduiEvents } from "./sdui/SduiEvents";
import { Screen, ScreenSection } from "@/types";

type LazyBlockProps = Element & {
  section?: string;
  element?: Element;
  loadingElement?: Element;
};

type LazySection = {
  section: ScreenSection;
  allSections: ScreenSection[];
};

export const LazyBlock = (props: RenderElementProps<LazyBlockProps>) => {
  const [lazySection, setLazySection] = useState<LazySection | null>(null);
  const [isLoading, setIsLoading] = useState(false);

  const handleSectionLoaded = (e: any) => {
    setIsLoading(false);
    var section = e.detail.screen.sections.find(
      (x: any) => x.name === props.element.section
    );
    if (section != null)
      setLazySection({
        allSections: e.detail.screen.sections as ScreenSection[],
        section: section,
      });
  };

  const handlSectionLoading = (e: any) => {
    const sections = e.detail.sections;
    const section = sections.find((x: string) => x == props.element.section);
    if (section) setIsLoading(true);
  };

  useEffect(() => {
    if (props.sections) {
      subscribe(SduiEvents.lazySectionsLoading, handlSectionLoading);
      subscribe(SduiEvents.lazySectionsLoaded, handleSectionLoaded);
    }

    return () => {
      if (props.sections) {
        unsubscribe(SduiEvents.lazySectionsLoading, handlSectionLoading);
        unsubscribe(SduiEvents.lazySectionsLoaded, handleSectionLoaded);
      }
    };
  }, []);

  return isLoading && props.element.loadingElement ? (
    <RenderElement
      element={props.element.loadingElement}
      sections={props.sections}
    />
  ) : lazySection ? (
    <RenderElement
      element={lazySection.section.element}
      sections={props.sections}
    />
  ) : props.element.element ? (
    <RenderElement element={props.element.element} sections={props.sections} />
  ) : (
    <div>Loading...</div>
  );
};
