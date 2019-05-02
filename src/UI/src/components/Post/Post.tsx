import React from 'react';
import { PostDto } from '@/api';
import { Divider, Tag, Tooltip } from 'antd';
import { Segment } from '@/components/Shared/Segment';
import moment from 'moment';

interface IPost {
  post: PostDto;
}

const Post: React.FC<IPost> = ({ post }) => {
  return (
    <Segment>
      <h3>{post.title}</h3>
      <Divider />
      <p>{post.content}</p>
      Теги: <Tag> {post.tag}</Tag>
      <Divider />
      <p>
        <Tooltip title={moment(post.date).format('YYYY-MM-DD HH:mm:ss')}>
          <span>Опубликовано {moment(post.date).fromNow()}</span>
        </Tooltip>
        <br />
        пользователем {post.username}
      </p>
    </Segment>
  );
};

export default Post;
