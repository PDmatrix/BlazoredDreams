/**
 * Routes:
 *   - ./src/routes/PrivateRoute.tsx
 */
import { BackTop, Divider, Pagination } from 'antd';
import React, { useEffect, useState } from 'react';
import useAuth from '@/hooks/useAuth';
import FetchWrapper from '@/components/Shared/FetchWrapper';
import UserInfo from '@/components/User/UserInfo';
import CreateDream from '@/components/User/CreateDream';
import DreamList from '@/components/User/DreamList';
import {
  DreamRequest,
  DreamsApi,
  PageOfDreamDto,
  PostRequest,
  PostsApi,
  UserDto,
  UsersApi,
} from '@/api';
import useNotification from '@/hooks/useNotification';
import Router from 'umi/router';

const User: React.FunctionComponent = () => {
  const auth = useAuth();
  const notification = useNotification();
  const [isLoading, setIsLoading] = useState(false);
  const [dreams, setDreams] = useState<PageOfDreamDto>({
    currentPage: 1,
    pageSize: 10,
    records: [],
    totalPages: 1,
  });
  const [user, setUser] = useState<UserDto>({
    email: '',
    userId: '',
    username: '',
  });
  const [page, setPage] = useState(1);

  const changePage = async (newPage: number) => {
    setPage(newPage);
    await getDreams(newPage);
  };

  const getDreams = async (page: number) => {
    const api = new DreamsApi({ apiKey: auth.getAccessToken() });
    const request = await api.dreamsGetAll(page);
    setDreams(request.data);
  };

  const getUser = async () => {
    const api = new UsersApi({ apiKey: auth.getAccessToken() });
    const request = await api.usersGetById();
    setUser(request.data);
  };

  useEffect(() => {
    setIsLoading(true);
    getDreams(page);
    getUser();
    setIsLoading(false);
  }, []);

  const createDream = async (dreamRequest: DreamRequest) => {
    const api = new DreamsApi({ apiKey: auth.getAccessToken() });
    await api.dreamsCreate(dreamRequest);
    await getDreams(1);
    notification.success('Сон добавлен');
  };

  const deleteDream = async (id: number) => {
    const api = new DreamsApi({ apiKey: auth.getAccessToken() });
    await api.dreamsDelete(id);
    await getDreams(1);
    notification.success('Сон удален');
  };

  const createPost = async (postRequest: PostRequest) => {
    const api = new PostsApi({ apiKey: auth.getAccessToken() });
    await api.postsCreate(postRequest);
    await getDreams(1);
    notification.success('Сон опубликован');
  };

  const RenderDreams = () => {
    if (!dreams.records) return null;

    return (
      <>
        {dreams.records.length > 0 && (
          <>
            <h3>Список снов:</h3>
            <DreamList createPost={createPost} deleteDream={deleteDream} dreams={dreams.records} />
            <Pagination onChange={changePage} current={page} total={dreams.totalPages * 10} />
          </>
        )}
      </>
    );
  };

  return (
    <FetchWrapper isLoading={isLoading}>
      <BackTop />
      <UserInfo email={user.email || ''} username={user.username || ''} />
      <CreateDream createDream={createDream} />
      <Divider />
      <RenderDreams />
    </FetchWrapper>
  );
};

export default User;
