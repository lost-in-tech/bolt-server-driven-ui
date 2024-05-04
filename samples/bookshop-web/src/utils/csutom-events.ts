export type PublishEventProps = {
  name: string;
  group?: string;
  data: unknown;
};
const publish = (props: PublishEventProps) => {
  const data = props?.data ?? {};
  const evnt = new CustomEvent(props.name, {
    detail: props ? { group: props.group, ...data } : {},
    bubbles: true,
    cancelable: true,
  } as CustomEventInit);
  document.dispatchEvent(evnt);
};

const subscribe = (
  eventName: string,
  listener: EventListenerOrEventListenerObject
) => {
  document.addEventListener(eventName, listener);
};

const unsubscribe = (
  eventName: string,
  listener: EventListenerOrEventListenerObject
) => {
  document.removeEventListener(eventName, listener);
};

export { publish, subscribe, unsubscribe };
