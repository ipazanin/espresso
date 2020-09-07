import React from 'react';
import cx from 'classnames';

import styles from './touchable.module.scss';

type ButtonProps = React.DetailedHTMLProps<
  React.ButtonHTMLAttributes<HTMLButtonElement>,
  HTMLButtonElement
>;

const Touchable: React.FC<ButtonProps> = ({
  onClick,
  children,
  className,
  type = 'button',
  ...props
}) => {
  const handleClick = React.useCallback(
    (e: React.MouseEvent<HTMLButtonElement>) => {
      if (onClick) {
        onClick(e);
      }
    },
    [onClick]
  );

  return (
    <button
      onClick={handleClick}
      type={type}
      className={cx(styles.container, className)}
      {...props}
    >
      {children}
    </button>
  );
};

export default Touchable;
