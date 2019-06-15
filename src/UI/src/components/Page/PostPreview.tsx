import { Divider, Tag, Tooltip, Typography } from 'antd';
import React from 'react';
import Link from 'umi/link';
import CommentIcon from '../Shared/CommentIcon';
import { Segment } from '../Shared/Segment';
import { PostPreviewDto, PostRequest } from '@/api';
import moment from 'moment';
import useAuth from '@/hooks/useAuth';

const { Title, Paragraph } = Typography;

interface IPostPreview {
  postPreview: PostPreviewDto;
  editPost: (postId: number, postRequest: PostRequest) => Promise<void>;
}

const PostPreview: React.FC<IPostPreview> = ({ postPreview, editPost }) => {
  const auth = useAuth();
  return (
    <Segment>
      <Link to={`/posts/${postPreview.id}`}>
        <Title level={4}>{postPreview.title}</Title>
      </Link>
      <p>
        <Tooltip title={moment(postPreview.date).format('YYYY-MM-DD HH:mm:ss')}>
          <span>Опубликовано {moment(postPreview.date).fromNow()}</span>
        </Tooltip>
        <br />
        пользователем {postPreview.username}
      </p>
      <Divider />
      <Paragraph
        {...postPreview.username === auth.getUserData().nickname && {
          editable: {
            onChange: value =>
              editPost(postPreview.id, { title: postPreview.title, excerpt: value }),
          },
        }}
      >
        {postPreview.excerpt}
      </Paragraph>
      Теги:{' '}
      {postPreview.tag.split(',').map((tag, idx) => {
        return <Tag key={idx}>{tag}</Tag>;
      })}
      <Divider />
      <Link to={`/posts/${postPreview.id}`}>
        <CommentIcon />
        {postPreview.comments}
      </Link>
    </Segment>
  );
};

export default PostPreview;
