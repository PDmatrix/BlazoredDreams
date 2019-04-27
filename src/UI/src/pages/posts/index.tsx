import { BackTop, Pagination, Spin } from 'antd';
import React, { useState, useEffect } from 'react';
import PostList from '../../components/Page/PostList';
import { PostsApi, PageOfPostPreviewDto } from '@/api';
import FetchWrapper from '@/components/Shared/FetchWrapper';

const Post: React.FC = () => {
  const [page, setPage] = useState(1);
  const [posts, setPosts] = useState<PageOfPostPreviewDto>({
    records: [],
    currentPage: 1,
    pageSize: 10,
    totalPages: 1,
  });
  const [isLoading, setIsLoading] = useState(true);

  const changePage = async (newPage: number) => {
    setPage(newPage);
    await GetPosts(newPage);
  };

  const GetPosts = async (page: number) => {
    setIsLoading(true);
    const api = new PostsApi();
    const request = await api.postsGetAll(page);
    setPosts(request.data);
    setIsLoading(false);
  };

  useEffect(() => {
    GetPosts(1);
  }, []);

  return (
    <FetchWrapper isLoading={isLoading}>
      <BackTop />
      <PostList posts={posts} />
      <Pagination onChange={changePage} current={page} total={posts.totalPages * 10} />
    </FetchWrapper>
  );
};

export default Post;
