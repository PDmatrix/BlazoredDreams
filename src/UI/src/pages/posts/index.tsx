import { BackTop, Pagination, Spin } from 'antd';
import axios from 'axios';
import React, { useState } from 'react';
import PostList from '../../components/Page/PostList';

const Post: React.FC = () => {
  const [page, setPage] = useState(1);

  return <div>Posts</div>;
};

export default Post;
