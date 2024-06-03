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
  element: Element;
  sectionsMap: Map<string, Element>;
};

export const LazyBlock = (props: RenderElementProps<LazyBlockProps>) => {
  const [lazySection, setLazySection] = useState<LazySection | null>(null);
  const [isLoading, setIsLoading] = useState(false);

  const handleSectionLoaded = (e: any) => {
    setIsLoading(false);

    var element = e.detail.sectionsMap.get(props.element.section);
    if (element != null)
      setLazySection({
        sectionsMap: e.detail.sectionsMap,
        element: element,
      });
  };

  const handlSectionLoading = (e: any) => {
    const sections = e.detail.sections;
    const section = sections.find((x: string) => x == props.element.section);
    if (section) setIsLoading(true);
  };

  useEffect(() => {
    if (props.sectionsMap) {
      subscribe(SduiEvents.lazySectionsLoading, handlSectionLoading);
      subscribe(SduiEvents.lazySectionsLoaded, handleSectionLoaded);
    }

    return () => {
      if (props.sectionsMap) {
        unsubscribe(SduiEvents.lazySectionsLoading, handlSectionLoading);
        unsubscribe(SduiEvents.lazySectionsLoaded, handleSectionLoaded);
      }
    };
  }, []);

  return isLoading && props.element.loadingElement ? (
    <RenderElement
      element={props.element.loadingElement}
      sectionsMap={props.sectionsMap}
    />
  ) : lazySection ? (
    <RenderElement
      element={lazySection.element}
      sectionsMap={lazySection.sectionsMap}
    />
  ) : props.element.element ? (
    <RenderElement
      element={props.element.element}
      sectionsMap={props.sectionsMap}
    />
  ) : (
    <div>Loading...</div>
  );
};
