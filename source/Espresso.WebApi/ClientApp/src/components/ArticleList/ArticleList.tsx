import React, { useEffect, useState } from 'react';
import axios from 'axios';

const ArticleList: React.FC = () => {
  const [state, setState] = useState({});
  useEffect(() => {
    axios
      .get(`http://localhost:8000/api/articles/latest`, {
        headers: {
          ['espresso-api-key']: 'b8b9cc0a-90f6-4aa3-96b6-c1d9bc7b15dd',
        },
      })
      .then((res) => {
        console.log(res);
        setState(res.data);
      });
  }, []);

  return <div></div>;
};

export default ArticleList;
