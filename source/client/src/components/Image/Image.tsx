import React, { useState } from 'react';

type HTMLImageProps = React.DetailedHTMLProps<
  React.ImgHTMLAttributes<HTMLImageElement>,
  HTMLImageElement
>;

export interface ImageProps extends HTMLImageProps {
  /**
   * Image source url.
   */
  src: string;
  /**
   * Image fallback source url (loads in case the original `src` is missing or invalid).
   */
  fallbackSrc?: string;
}

/**
 * UI component that renders an image with a <img> tag.
 */
const Image: React.FC<ImageProps> = ({ src, fallbackSrc, alt, ...props }) => {
  const [imageSrc, setImageSrc] = useState<string>(src);

  const handleError = React.useCallback(() => {
    if (fallbackSrc) {
      setImageSrc(fallbackSrc);
    }
  }, [fallbackSrc]);

  return (
    <img
      {...props}
      src={imageSrc}
      {...(fallbackSrc ? { 'data-fallback-src': fallbackSrc } : {})}
      alt={alt}
      onError={handleError}
    />
  );
};

export default React.memo(Image);
