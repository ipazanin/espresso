import React from 'react';
import cx from 'classnames';

import { Flex, Text } from '@profico/react-ui-components';
import { useFullLifecyle } from '@profico/react-utils';

import styles from './loader.module.scss';

interface LoadingSpinnerProps
  extends React.DetailedHTMLProps<
    React.SVGProps<SVGSVGElement>,
    SVGSVGElement
  > {
  size?: number;
}

export const LoadingSpinner: React.FC<LoadingSpinnerProps> = React.memo(
  ({ className, size = 48, ...props }) => (
    <svg
      className={cx(styles.loadingSpinner, className)}
      width={`${size}px`}
      height={`${size}px`}
      viewBox="0 0 66 66"
      xmlns="http://www.w3.org/2000/svg"
      {...props}
    >
      <circle
        className={styles.circle}
        fill="none"
        strokeWidth="6"
        strokeLinecap="round"
        cx="33"
        cy="33"
        r="30"
      />
    </svg>
  )
);

const Loader: React.FC = () => {
  useFullLifecyle(
    () => {
      document.body.style.overflow = 'hidden';
    },
    () => {
      document.body.style.removeProperty('overflow');
    }
  );

  return (
    <Flex
      className={styles.loader}
      flexDirection="column"
      justifyContent="center"
      alignItems="center"
    >
      <LoadingSpinner className={styles.spinner} />
      <Text className={styles.text} align="center" weight="semibold" size="h3">
        Učitavanje...
      </Text>
    </Flex>
  );
};

export default React.memo(Loader);
