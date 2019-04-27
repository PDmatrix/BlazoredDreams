import React from 'react';
import { PostDto } from '@/api';
import { Divider, Tag } from 'antd';
import { Segment } from '@/components/Shared/Segment';

interface IPost {
  post: PostDto;
}

const Post: React.FC<IPost> = ({ post }) => {
  return (
    <Segment>
      <h3>{post.title}</h3>
      <p>
        Опубликовано {post.date}
        пользователем {post.username}
      </p>
      <Divider />
      <p>{post.content}</p>
      Теги: <Tag> {post.tag}</Tag>
      <Divider />
    </Segment>
  );
};

export default Post;
