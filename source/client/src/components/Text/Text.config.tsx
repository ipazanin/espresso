/* eslint-disable react/require-default-props */
import React from 'react';

export type TextColor =
  | 'primary'
  | 'secondary'
  | 'muted'
  | 'white'
  | 'black'
  | 'error'
  | 'success';
export type TextSize = 'h1' | 'h2' | 'h3' | 'p' | 'caption' | 'small';
export type TextWeight = 'light' | 'normal' | 'semibold' | 'bold';

type TextConfigSizes = {
  [key in TextSize]: Pick<React.CSSProperties, 'fontSize' | 'lineHeight'>;
};
type TextConfigWeights = {
  [key in TextWeight]: 100 | 200 | 300 | 400 | 500 | 600 | 700 | 800 | 900;
};

export interface TextConfigContextValues {
  fontFamily?: string;
  /**
   * Map of text colors with keys `primary`, `secondary`, `muted`, `white`, `black`, `error`, `success`
   */
  colors?: Record<TextColor, string>;
  /**
   * Map of text sizes with keys `h1`, `h2`, `h3`, `p`, `caption`, `small`
   */
  sizes?: Partial<TextConfigSizes>;
  /**
   * Map of font weights with keys `normal`, `semibold`, `bold`
   */
  weights?: Partial<TextConfigWeights>;
}

export const defaultTextSizes: TextConfigSizes = {
  caption: {
    fontSize: '.9375rem',
    lineHeight: '1.125rem',
  },
  h1: {
    fontSize: '2.25rem',
    lineHeight: '2.6875rem',
  },
  h2: {
    fontSize: '1.6875rem',
    lineHeight: '2.0625rem',
  },
  h3: {
    fontSize: '1.375rem',
    lineHeight: '1.625rem',
  },
  p: {
    fontSize: '1.0625rem',
    lineHeight: '1.5rem',
  },
  small: {
    fontSize: '.6875rem',
    lineHeight: '1rem',
  },
};

export const defaultTextWeights: TextConfigWeights = {
  light: 300,
  bold: 700,
  semibold: 500,
  normal: 400,
};

const defaultFontFamily =
  '-apple-system, BlinkMacSystemFont, Segoe UI, Roboto, Open Sans, Helvetica Neue, Helvetica, Arial, sans-serif';
const defaultTextColors: Record<TextColor, string> = {
  primary: '#080357',
  secondary: '#F7F7F7',
  muted: '#A1AFBD',
  error: '#EB550A',
  success: '#009F75',
  black: '#000000',
  white: '#FFFFFF',
};

export const defaultContextValues: TextConfigContextValues = {
  fontFamily: defaultFontFamily,
  colors: defaultTextColors,
  sizes: defaultTextSizes,
  weights: defaultTextWeights,
};

const TextConfigContext = React.createContext<TextConfigContextValues>(
  defaultContextValues
);

export interface TextConfigProviderProps {
  children: React.ReactNode;
  /**
   * Provider config values.
   */
  config?: TextConfigContextValues;
}

export const TextConfigProvider: React.FC<TextConfigProviderProps> = ({
  children,
  config,
}) => (
  <TextConfigContext.Provider
    value={{
      colors: { ...defaultTextColors, ...(config ? config.colors : {}) },
      sizes: { ...defaultTextSizes, ...(config ? config.sizes : {}) },
      weights: { ...defaultTextWeights, ...(config ? config.weights : {}) },
      fontFamily:
        config && config.fontFamily ? config.fontFamily : defaultFontFamily,
    }}
  >
    {children}
  </TextConfigContext.Provider>
);
export const useTextConfig = () => React.useContext(TextConfigContext);
