import React from 'react';

type UseWillUnmountHook = (callback: () => void) => void;

const useWillUnmount: UseWillUnmountHook = callback => {
  // eslint-disable-next-line react-hooks/exhaustive-deps
  React.useEffect(() => callback, []);
};

export default useWillUnmount;
