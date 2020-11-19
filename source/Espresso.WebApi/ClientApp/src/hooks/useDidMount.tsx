import React from 'react';

type UseDidMountHook = (callback: () => void) => void;

const useDidMount: UseDidMountHook = callback => {
  const savedCallback = React.useRef<() => void>(callback);

  React.useEffect(() => {
    savedCallback.current();
  }, []);
};

export default useDidMount;
