import React from 'react';

type UseDocumentTitleHook = (title: string | (() => string)) => void;

const useDocumentTitle: UseDocumentTitleHook = title => {
  React.useEffect(() => {
    document.title = typeof title === 'string' ? title : title();
  }, [title]);
};

export default useDocumentTitle;
