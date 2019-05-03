import React from 'react';
import { PostDto } from '@/api';
import { Divider, Tag, Tooltip, Typography } from 'antd';
import { Segment } from '@/components/Shared/Segment';
import moment from 'moment';

interface IPost {
  post: PostDto;
}

const Post: React.FC<IPost> = ({ post }) => {
  return (
    <Segment>
      <Typography>
        <Typography.Title level={4}>{post.title}</Typography.Title>
        <Divider />
        <Typography.Paragraph>{post.content}</Typography.Paragraph>
        Теги: <Tag> {post.tag}</Tag>
        <Divider />
        <Typography.Paragraph>
          <Tooltip title={moment(post.date).format('YYYY-MM-DD HH:mm:ss')}>
            <span>Опубликовано {moment(post.date).fromNow()}</span>
          </Tooltip>
          <br />
          пользователем {post.username}
        </Typography.Paragraph>
      </Typography>
    </Segment>
  );
};

export default Post;
