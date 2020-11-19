/* eslint-disable react/prefer-stateless-function */

import React from 'react';
import cx from 'classnames';

import { css, StyleDeclaration, StyleSheet } from 'aphrodite/no-important';

import styles from './Flex.style';

type HTMLDivProps = React.HTMLProps<HTMLDivElement>;
interface BaseProps {
  /**
   * Expand to full width.
   */
  fluid?: boolean;
  /**
   * Set `flex` CSS property to `1`.
   */
  flexOut?: boolean;
  /**
   * Determines how content is distributed along the main axis.
   */
  justifyContent?:
    | 'center'
    | 'flex-start'
    | 'flex-end'
    | 'space-evenly'
    | 'space-between'
    | 'space-around';
  /**
   * Determines how content is distributed inside its alignment container (along the main axis).
   */
  justifySelf?:
    | 'center'
    | 'flex-start'
    | 'flex-end'
    | 'space-evenly'
    | 'space-between'
    | 'space-around';
  /**
   * Determines how content is distributed along the secondary axis.
   */
  alignItems?: 'flex-start' | 'flex-end' | 'center';
  /**
   * Determines how content is distributed inside its alignment container (along the secondary axis).
   */
  alignSelf?: 'flex-start' | 'flex-end' | 'center';
  /**
   * Determines how items are placed inside a flex container, defining the main axis and direction.
   */
  flexDirection?: 'row' | 'row-reverse' | 'column' | 'column-reverse';
  /**
   * Determines if items are forced onto one line or can wrap onto multiple lines.
   */
  flexWrap?: 'nowrap' | 'wrap' | 'wrap-reverse';
  /**
   * Ignores all passed style props (except `className`) and renders a `<div>`.
   */
  disableStyles?: boolean;
  ref?:
    | ((instance: HTMLDivElement | null) => void)
    | React.MutableRefObject<HTMLDivElement | null>
    | null;
}
export type FlexProps = Omit<HTMLDivProps, 'ref'> & BaseProps;
type DynamicStyles = Omit<FlexProps, 'fluid' | 'flexOut' | 'disableStyles'>;

export const createDynamicStyles = (
  property: keyof DynamicStyles,
  value: string
) =>
  StyleSheet.create({
    [property]: {
      [property]: value,
    },
  })[property];

/**
 * UI component that controls the layout of its children.
 */
const Flex = React.forwardRef<HTMLDivElement, FlexProps>(
  (
    {
      fluid,
      flexOut,
      justifyContent,
      justifySelf,
      alignItems,
      alignSelf,
      flexWrap,
      flexDirection = 'row',
      className,
      disableStyles = false,
      ...props
    },
    ref
  ) => {
    let classes: StyleDeclaration[] = disableStyles
      ? []
      : [styles.dFlex, createDynamicStyles('flexDirection', flexDirection)];

    if (!disableStyles) {
      if (justifyContent) {
        classes = [
          ...classes,
          createDynamicStyles('justifyContent', justifyContent),
        ];
      }
      if (justifySelf) {
        classes = [...classes, createDynamicStyles('justifySelf', justifySelf)];
      }
      if (alignItems) {
        classes = [...classes, createDynamicStyles('alignItems', alignItems)];
      }
      if (alignSelf) {
        classes = [...classes, createDynamicStyles('alignSelf', alignSelf)];
      }
      if (flexWrap) {
        classes = [...classes, createDynamicStyles('flexWrap', flexWrap)];
      }
      if (fluid) {
        classes = [...classes, styles.fluid];
      }
      if (flexOut) {
        classes = [...classes, styles.flexOut];
      }
    }

    return (
      <div {...props} ref={ref} className={cx(css(...classes), className)} />
    );
  }
);

export default Flex;
