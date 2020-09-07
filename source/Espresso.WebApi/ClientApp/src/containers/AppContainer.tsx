/* eslint-disable sort-keys */
import React from 'react';
import Navbar from 'components/Navbar';
import Main from 'views/Main';
import Loader from 'components/Loader';

import { useParams } from 'react-router-dom';
import { useDocumentTitle } from '@profico/react-utils';
import {
  GetWebConfigurationCategory,
  GetWebConfigurationQueryResponse,
} from 'models';
import { useQuery } from '@apollo/client';
import { GET_CONFIGURATION } from 'graphql/queries/get_configuration';

const filterCategories = (allCategories: GetWebConfigurationCategory[]) =>
  allCategories.filter(({ name }) => name !== 'Lokalno');

const getCurrentCategory = (
  path: string,
  allRoutes: string[],
  allCategories: GetWebConfigurationCategory[]
): GetWebConfigurationCategory | undefined => {
  const currentRouteIndex = allRoutes.findIndex(route => route === path);

  if (currentRouteIndex !== -1) {
    const category = allCategories[currentRouteIndex];

    if (category) {
      return category;
    }
  }

  return undefined;
};

const AppContainer: React.FC = () => {
  const { path = '' } = useParams<{ path?: string }>();
  const { loading: loadingConfiguration, data: config } = useQuery<{
    webConfiguration: GetWebConfigurationQueryResponse;
  }>(GET_CONFIGURATION.gql);

  const {
    allCategories,
    allRoutes,
    newsPortalIds,
    currentCategory,
  } = React.useMemo(() => {
    const filteredCategories = config
      ? filterCategories(config.webConfiguration.categories)
      : [];
    const routes = filteredCategories.map(({ url }) => url.replace('/', ''));
    const category = getCurrentCategory(path, routes, filteredCategories);

    return {
      allCategories: filteredCategories,
      allRoutes: routes,
      newsPortalIds: config ? config.webConfiguration.newsPortalIds : [],
      currentCategory: category,
    };
  }, [config, path]);

  useDocumentTitle(() => {
    const baseTitle = 'Espresso';

    return currentCategory
      ? `${currentCategory.name} - ${baseTitle}`
      : baseTitle;
  });

  // const handleRouteChange = React.useCallback(
  //   (newRoute: string) => {
  //     setCurrentCategory(
  //       getCurrentCategory(newRoute, allRoutes, allCategories)
  //     );
  //   },
  //   [allCategories, allRoutes]
  // );

  if (loadingConfiguration) {
    return <Loader />;
  }

  if (!currentCategory || allRoutes.length === 0 || !allRoutes.includes(path)) {
    return <div>Page not found.</div>;
  }

  return (
    <>
      <Navbar
        categories={allCategories}
        routes={allRoutes}
        onRouteChange={() => {}}
      />
      <Main newsPortalIds={newsPortalIds} category={currentCategory} />
    </>
  );
};

export default AppContainer;
