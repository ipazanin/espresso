import React from 'react';
import cx from 'classnames';

import { css, StyleSheet } from 'aphrodite/no-important';

import {
  TextColor,
  useTextConfig,
  defaultTextSizes,
  defaultTextWeights,
  TextConfigContextValues,
  TextSize,
  TextWeight,
} from './Text.config';

type HTMLTextElements =
  | HTMLSpanElement
  | HTMLParagraphElement
  | HTMLHeadingElement;
export type TextStyle = 'normal' | 'italic' | 'oblique';
export type TextTransform = 'uppercase' | 'lowercase' | 'none' | 'unset';
export type TextAlign = 'left' | 'center' | 'right' | 'justify';
export type TextDecoration = 'none' | 'underline' | 'dotted' | 'line-through';

interface TextModifiers {
  /**
   * Adjust the font style.
   */
  fontStyle?: TextStyle;
  /**
   * Adjust the font size.
   */
  size?: TextSize;
  /**
   * Adjust the font weight.
   */
  weight?: TextWeight;
  /**
   * Transforms the text.
   */
  transform?: TextTransform;
  /**
   * Aligns the text.
   */
  align?: TextAlign;
  /**
   * Sets the text decoration.
   */
  decoration?: TextDecoration;
  /**
   * Sets the text color.
   */
  color?: TextColor;
  /**
   * Wraps the text overflow with 3 dots (...).
   */
  ellipsis?: boolean;
}

export interface TextProps
  extends TextModifiers,
    Omit<
      React.DetailedHTMLProps<
        React.HTMLAttributes<HTMLTextElements>,
        HTMLTextElements
      >,
      'color' | 'size' | 'ref'
    > {
  className?: string;
  component?: React.ComponentType<
    React.DetailedHTMLProps<React.HTMLAttributes<HTMLElement>, HTMLElement>
  >;
  children?: React.ReactNode;
}

const fontStyleMap: { [key in TextStyle]: TextStyle } = {
  normal: 'normal',
  italic: 'italic',
  oblique: 'oblique',
};

export const createStyles = (
  { colors, sizes, weights, fontFamily }: TextConfigContextValues,
  modifiers: TextModifiers
) =>
  StyleSheet.create({
    mutual: {
      color: colors ? colors.primary : '#000000',
      fontFamily,
      ...defaultTextSizes.p,
      fontStyle: 'normal',
      fontWeight: defaultTextWeights.normal,
    },
    ...(modifiers.size && sizes ? { size: sizes[modifiers.size] } : {}),
    ...(modifiers.weight && weights
      ? { weight: { fontWeight: weights[modifiers.weight] } }
      : {}),
    ...(modifiers.fontStyle
      ? { fontStyle: { fontStyle: fontStyleMap[modifiers.fontStyle] } }
      : {}),
    ...(modifiers.transform
      ? {
          transform: {
            textTransform: modifiers.transform,
          },
        }
      : {}),
    ...(modifiers.align
      ? {
          align: {
            textAlign: modifiers.align,
          },
        }
      : {}),
    ...(modifiers.decoration
      ? {
          decoration: {
            textDecoration: modifiers.decoration,
          },
        }
      : {}),
    ...(modifiers.color && colors
      ? {
          color: {
            color: colors[modifiers.color],
          },
        }
      : {}),
    ...(modifiers.ellipsis
      ? {
          ellipsis: {
            textOverflow: 'ellipsis',
            overflow: 'hidden',
            whiteSpace: 'nowrap',
          },
        }
      : {}),
  });

export const textComponentMap: { [key in TextSize]: React.ElementType } = {
  small: 'small',
  caption: 'span',
  h1: 'h1',
  h2: 'h2',
  h3: 'h3',
  p: 'p',
};

const Text = React.forwardRef<HTMLTextElements, TextProps>(
  (
    {
      children,
      className,
      align,
      transform,
      decoration,
      color,
      component: ReplacementComponent,
      size,
      weight,
      fontStyle,
      ellipsis,
      ...props
    },
    ref
  ) => {
    const textConfig = useTextConfig();
    const styles = createStyles(textConfig, {
      align,
      transform,
      decoration,
      color,
      size,
      weight,
      fontStyle,
      ellipsis,
    });
    const generatedStyles = css(
      [
        styles.mutual,
        styles.size,
        styles.weight,
        styles.fontStyle,
        styles.transform,
        styles.align,
        styles.decoration,
        styles.color,
        styles.ellipsis,
      ].filter(Boolean)
    );

    if (ReplacementComponent) {
      return (
        <ReplacementComponent
          ref={ref}
          className={cx(generatedStyles, className)}
          {...props}
        >
          {children}
        </ReplacementComponent>
      );
    }

    const Component = textComponentMap[size || 'p'];

    return (
      <Component
        ref={ref}
        className={cx(generatedStyles, className)}
        {...props}
      >
        {children}
      </Component>
    );
  }
);

export default Text;
