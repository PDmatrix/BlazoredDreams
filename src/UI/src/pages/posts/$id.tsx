import React, { useEffect, useState } from 'react';
import { RouteComponentProps } from 'react-router';
import Post from '@/components/Post/Post';
import {
  BASE_PATH,
  CommentDto,
  CommentRequest,
  CommentsApi,
  PostDto,
  PostsApi,
  UserDto,
  UsersApi,
} from '@/api';
import FetchWrapper from '@/components/Shared/FetchWrapper';
import CommentList from '@/components/Post/CommentList';
import useNotification from '@/hooks/useNotification';
import useAuth from '@/hooks/useAuth';
import axios from 'axios';

const PostEntry: React.FC<RouteComponentProps<{ id: string }>> = props => {
  const auth = useAuth();
  const notification = useNotification();
  const [post, setPost] = useState<PostDto>({
    comments: 0,
    tag: 'tag',
    date: new Date().toString(),
    content: 'content',
    id: 1,
    title: 'title',
    username: 'username',
  });
  const [user, setUser] = useState<UserDto>({
    avatar: '',
    username: '',
    userId: '',
    email: '',
  });
  const [comments, setComments] = useState<CommentDto[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const fetchData = async (id: number) => {
    setIsLoading(true);
    const postsApi = new PostsApi();
    const postsRequest = await postsApi.postsGetById(id);
    setPost(postsRequest.data);
    const commentsApi = new CommentsApi();
    const commentsRequest = await commentsApi.commentsGetAll(id);
    setComments(commentsRequest.data);
    setIsLoading(false);
  };
  useEffect(() => {
    fetchData(Number(props.match.params.id));
    getUser();
  }, []);

  const addComment = async (commentRequest: CommentRequest) => {
    const api = new CommentsApi({ apiKey: auth.getAccessToken() });
    await api.commentsCreate(Number(props.match.params.id), commentRequest);
    await fetchData(Number(props.match.params.id));
    notification.success('Комментарий добавлен');
  };

  const deleteComment = async (commentId: number) => {
    const api = new CommentsApi({ apiKey: auth.getAccessToken() });
    await api.commentsDelete(commentId, props.match.params.id);
    await fetchData(Number(props.match.params.id));
    notification.success('Комментарий удален');
  };

  const editComment = async (commentId: number, commentRequest: CommentRequest) => {
    const api = new CommentsApi({ apiKey: auth.getAccessToken() });
    await api.commentsUpdate(commentId, props.match.params.id, commentRequest);
    await fetchData(Number(props.match.params.id));
    notification.success('Комментарий изменен');
  };

  const getUser = async () => {
    if (!auth.isAuthenticated()) return;
    const api = new UsersApi({ apiKey: auth.getAccessToken() });
    const request = await api.usersGetById();
    setUser(request.data);
  };

  const changeUserImage = (postId: number) => {
    return async (info: any) => {
      if (info.file.status !== 'done') return;
      const formData = new FormData();
      formData.append('file', info.file.originFileObj);
      const response = await axios({
        method: 'put',
        url: `${BASE_PATH}/api/posts/${postId}/image`,
        data: formData,
        headers: { 'Content-Type': 'multipart/form-data', Authorization: auth.getAccessToken() },
      });
      if (response.status === 401) auth.login();
      else {
        notification.success('Обложка добавлена!');
        await fetchData(Number(props.match.params.id));
      }
    };
  };

  return (
    <FetchWrapper isLoading={isLoading}>
      <Post changeUserImage={changeUserImage} post={post} />
      <CommentList
        editComment={editComment}
        deleteComment={deleteComment}
        addComment={addComment}
        comments={comments}
        avatar={user.avatar || ''}
      />
    </FetchWrapper>
  );
};

export default PostEntry;
