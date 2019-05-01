/**
 * Routes:
 *   - ./src/routes/PrivateRoute.tsx
 */
import { BackTop, Button, Divider, Icon, Pagination, Upload } from 'antd';
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
  BASE_PATH,
} from '@/api';
import useNotification from '@/hooks/useNotification';
import axios from 'axios';

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

  const upload = async (info: any) => {
    if (info.file.status !== 'done') return;
    const formData = new FormData();
    formData.append('file', info.file.originFileObj);
    await axios({
      method: 'post',
      url: `${BASE_PATH}/api/users/image`,
      data: formData,
      headers: { 'Content-Type': 'multipart/form-data', Authorization: auth.getAccessToken() },
    });
    await getUser();
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

  const props = {
    name: 'file',
    onChange: upload,
    showUploadList: false,
  };

  return (
    <FetchWrapper isLoading={isLoading}>
      <BackTop />
      <Upload {...props}>
        <Button>
          <Icon type="upload" /> Click to Upload
        </Button>
      </Upload>
      <UserInfo
        avatar={user.avatar || ''}
        email={user.email || ''}
        username={user.username || ''}
      />
      <CreateDream createDream={createDream} />
      <Divider />
      <RenderDreams />
    </FetchWrapper>
  );
};

export default User;
