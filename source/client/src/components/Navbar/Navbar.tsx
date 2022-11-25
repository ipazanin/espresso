/* eslint-disable sort-keys */
import React from 'react';
import Touchable from 'components/Touchable';
import cx from 'classnames';

import {
  NavLink,
  useParams,
  useHistory,
  Link,
  useLocation,
} from 'react-router-dom';
import { ArrowRight } from 'components/Icons';
import { GetWebConfigurationCategory } from 'models';
import Flex from '../Flex/Flex';
import Text from '../Text/Text';

import styles from './navbar.module.scss';

interface NavbarProps {
  categories: GetWebConfigurationCategory[];
  routes: string[];
}

enum NavArrowActions {
  FORWARDS = 'FORWARDS',
  BACKWARDS = 'BACKWARDS',
}

const findNextRoute = (
  currentRoute: string,
  action: NavArrowActions,
  allRoutes: string[]
): string | null => {
  const currentIndex = allRoutes.findIndex(route => route === currentRoute);

  if (currentIndex === -1) {
    return null;
  }

  if (
    (currentIndex === 0 && action === NavArrowActions.BACKWARDS) ||
    (currentIndex === allRoutes.length - 1 &&
      action === NavArrowActions.FORWARDS)
  ) {
    return null;
  }

  const nextIndex =
    action === NavArrowActions.BACKWARDS ? currentIndex - 1 : currentIndex + 1;
  const nextRoute = allRoutes[nextIndex];

  return nextRoute;
};

const Navbar: React.FC<NavbarProps> = ({ categories, routes }) => {
  const history = useHistory();
  const location = useLocation();

  const { path = '' } = useParams<{ path: string }>();

  const pushToNextRoute = React.useCallback(
    (nextRoute: string) => {
      history.push(nextRoute);
    },
    [history]
  );

  const handleArrowClick = (e: React.MouseEvent<SVGSVGElement>) => {
    const {
      currentTarget: {
        dataset: { action },
      },
    } = e;

    if (!action) {
      return;
    }

    const nextRoute = findNextRoute(path, action as NavArrowActions, routes);

    if (nextRoute === null) {
      return;
    }

    pushToNextRoute(nextRoute);
  };

  React.useEffect(() => {
    const handleKeydown = (e: KeyboardEvent) => {
      const { key } = e;

      if (key === 'ArrowLeft' || key === 'ArrowRight') {
        const nextRoute = findNextRoute(
          path,
          key === 'ArrowLeft'
            ? NavArrowActions.BACKWARDS
            : NavArrowActions.FORWARDS,
          routes
        );

        if (nextRoute === null) {
          return;
        }

        pushToNextRoute(nextRoute);
      }
    };

    window.addEventListener('keydown', handleKeydown);

    return () => {
      window.removeEventListener('keydown', handleKeydown);
    };
  }, [history, path, routes, pushToNextRoute]);

  const handleGoToHomeClick = React.useCallback(
    (e: React.MouseEvent<HTMLAnchorElement>) => {
      e.preventDefault();

      if (location.pathname !== '/') {
        // history.push('/');
        pushToNextRoute('/');
      }
    },
    [location.pathname, pushToNextRoute]
  );

  const handleNavItemClick = (e: React.MouseEvent<HTMLAnchorElement>) => {
    e.preventDefault();

    const {
      currentTarget: {
        dataset: { url },
      },
    } = e;

    // Empty string is a valid option, that's why we can't have a simple check like `if (!url) {...}`
    if (typeof url !== 'undefined') {
      pushToNextRoute(url);
    }
  };

  return (
    <Flex
      className={styles.container}
      justifyContent="space-between"
      alignItems="center"
      fluid
    >
      <Link to="/" className={styles.logoLink} onClick={handleGoToHomeClick}>
        <img src="/logo512.png" alt="Espresso logo" className={styles.logo} />
      </Link>
      <Flex className={styles.items}>
        {routes.map((href, index) => {
          const category = categories[index];

          return (
            <NavLink
              key={category.id}
              to={href}
              data-url={href}
              isActive={() => path === href}
              activeClassName={styles.navItem__active}
              className={styles.navItem}
              onClick={handleNavItemClick}
            >
              <Text
                className={cx(styles.navItemText, {
                  [styles.navItem__activeText]: path === href,
                })}
                weight="semibold"
                size="caption"
              >
                {category.name}
              </Text>
            </NavLink>
          );
        })}
      </Flex>
      <Flex className={styles.arrowsWrapper}>
        <Touchable disabled={path === ''} className={styles.arrowBtn}>
          <ArrowRight
            className={styles.arrowLeft}
            data-action={NavArrowActions.BACKWARDS}
            onClick={handleArrowClick}
          />
        </Touchable>
        <Touchable
          disabled={path === routes[routes.length - 1]}
          className={styles.arrowBtn}
        >
          <ArrowRight
            data-action={NavArrowActions.FORWARDS}
            onClick={handleArrowClick}
          />
        </Touchable>
      </Flex>
    </Flex>
  );
};

export default Navbar;
