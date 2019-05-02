import React, { useEffect, useState } from 'react';
import { RouteComponentProps } from 'react-router';
import Post from '@/components/Post/Post';
import {
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

  return (
    <FetchWrapper isLoading={isLoading}>
      <Post post={post} />
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
