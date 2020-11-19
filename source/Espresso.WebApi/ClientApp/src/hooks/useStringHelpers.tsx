import React from 'react';

interface UseStringHelpersPayload {
  camelCaseToSnakeCase: (word: string) => string;
  hexToRgba: (hex: string, alpha?: number) => string;
  isUuid: (str: string) => boolean;
  snakeCaseToCamelCase: (word: string) => string;
}

type UseStringHelpersHook = () => UseStringHelpersPayload;

const useStringHelpers: UseStringHelpersHook = () => {
  const snakeCaseToCamelCase = React.useCallback(
    (word: string) =>
      word.toLowerCase().replace(/([-_]\w)/g, g => g[1].toUpperCase()),
    []
  );

  const camelCaseToSnakeCase = React.useCallback(
    (word: string) => word.replace(/[A-Z]/g, g => `_${g.toLowerCase()}`),
    []
  );

  const isUuid = React.useCallback(
    (str: string) =>
      /^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i.test(
        str
      ),
    []
  );

  const hexToRgba = React.useCallback((hex: string, alpha?: number) => {
    const r = parseInt(hex.slice(1, 3), 16);
    const g = parseInt(hex.slice(3, 5), 16);
    const b = parseInt(hex.slice(5, 7), 16);

    if (alpha) {
      return `rgba(${r}, ${g}, ${b}, ${alpha})`;
    }

    return `rgb(${r}, ${g}, ${b})`;
  }, []);

  return {
    camelCaseToSnakeCase,
    hexToRgba,
    isUuid,
    snakeCaseToCamelCase,
  };
};

export default useStringHelpers;
