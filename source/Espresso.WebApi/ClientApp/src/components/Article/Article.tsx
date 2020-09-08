import React from 'react';
import moment from 'moment';
import cx from 'classnames';

import { Flex, Text, Image } from '@profico/react-ui-components';
import { GetLatestArticlesArticle, GetFeaturedArticlesArticle } from 'models';

import styles from './article.module.scss';

type AnchorElementProps = React.DetailedHTMLProps<
  React.HTMLProps<HTMLAnchorElement>,
  HTMLAnchorElement
>;
interface LatestArticleProps extends AnchorElementProps {
  article: GetLatestArticlesArticle;
}
interface FeaturedArticleProps extends AnchorElementProps {
  article: GetFeaturedArticlesArticle;
  variant: 'main' | 'regular';
}

const parseTimeFromUtc = (dateTime: string) => moment(`${dateTime}Z`);

const WrapperLink: React.FC<{ href: string; className?: string }> = ({
  href,
  className,
  children,
}) => (
  <a
    href={href}
    className={cx(styles.wrapperLink, className)}
    target="_blank"
    rel="noopener noreferrer"
  >
    {children}
  </a>
);

const Featured: React.FC<FeaturedArticleProps> = ({
  article,
  className,
  variant,
  ...props
}) => {
  const { url, imageUrl, title, newsPortal, publishDateTime } = article;

  if (variant === 'main') {
    return (
      <WrapperLink
        {...props}
        href={url}
        className={cx(styles.featuredMain, className)}
      >
        <Image
          src={imageUrl || '/assets/images/logo.png'}
          fallbackSrc="/assets/images/logo.png"
          className={styles.coverImage}
        />
        <Flex flexDirection="column">
          <Text
            title={title}
            className={styles.title}
            weight="bold"
            align="left"
            size="h1"
          >
            {title.length > 120
              ? `${title.substring(0, 120).trim()}...`
              : title}
          </Text>
          <Flex alignItems="center">
            <Image
              src={newsPortal.iconUrl || '/assets/images/logo.png'}
              fallbackSrc="/assets/images/logo.png"
              className={styles.newsPortalIcon}
            />
            <Text size="caption">
              {`${newsPortal.name} • ${parseTimeFromUtc(
                publishDateTime
              ).fromNow()}`}
            </Text>
          </Flex>
        </Flex>
      </WrapperLink>
    );
  }

  return (
    <WrapperLink
      {...props}
      href={url}
      className={cx(styles.featuredRegular, className)}
    >
      <Flex className={styles.coverImageWrapper}>
        <Flex className={styles.newsPortalWrapper} alignItems="center">
          <Image
            src={newsPortal.iconUrl || '/assets/images/logo.png'}
            fallbackSrc="/assets/images/logo.png"
            className={styles.newsPortalIcon}
          />
          <Text size="small" color="white">
            {newsPortal.name}
          </Text>
        </Flex>
        <Flex
          className={styles.coverImage}
          style={{
            backgroundImage: `url(${imageUrl || '/assets/images/logo.png'})`,
          }}
          disableStyles
        />
      </Flex>
      <Text title={title} className={styles.title} weight="bold" align="left">
        {title.length > 80 ? `${title.substring(0, 80).trim()}...` : title}
      </Text>
    </WrapperLink>
  );
};

const Latest: React.FC<LatestArticleProps> = ({
  article: { url, imageUrl, title, newsPortal, publishDateTime },
  className,
  ...props
}) => (
  <WrapperLink {...props} href={url} className={cx(styles.latest, className)}>
    <Image
      src={imageUrl || '/assets/images/logo.png'}
      fallbackSrc="/assets/images/logo.png"
      className={styles.coverImage}
    />
    <Flex flexDirection="column" className={styles.textWrapper}>
      <Text title={title} className={styles.title} weight="bold" align="left">
        {title.length > 80 ? `${title.substring(0, 80).trim()}...` : title}
      </Text>
      <Text size="caption" align="left">
        {`${newsPortal.name} • ${parseTimeFromUtc(publishDateTime).fromNow()}`}
      </Text>
    </Flex>
  </WrapperLink>
);

export default {
  Featured,
  Latest,
};
