import React, { useState } from 'react';

interface UseWindowDimensionsPayload {
  width: number;
  height: number;
}

type UseWindowDimensionsHook = () => UseWindowDimensionsPayload;

const useWindowDimensions: UseWindowDimensionsHook = () => {
  const [size, setSize] = useState<UseWindowDimensionsPayload>({
    width: window.innerWidth,
    height: window.innerHeight,
  });

  React.useEffect(() => {
    const handleWindowResize = () => {
      setSize({ width: window.innerWidth, height: window.innerHeight });
    };

    window.addEventListener('resize', handleWindowResize);

    return () => {
      window.removeEventListener('resize', handleWindowResize);
    };
  }, []);

  return size;
};

export default useWindowDimensions;
