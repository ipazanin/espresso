import React from 'react';

type UseDidUpdateHook = (
  callback: () => void,
  deps: React.DependencyList
) => void;

const useDidUpdate: UseDidUpdateHook = (callback, deps) => {
  const savedCallback = React.useRef<() => void>(callback);

  React.useEffect(() => {
    savedCallback.current = callback;
  }, [callback]);

  React.useEffect(() => {
    savedCallback.current();
  }, [deps]);
};

export default useDidUpdate;
