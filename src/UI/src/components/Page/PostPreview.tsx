import { Divider, Tag, Tooltip } from 'antd';
import React from 'react';
import Link from 'umi/link';
import CommentIcon from '../Shared/CommentIcon';
import { Segment } from '../Shared/Segment';
import { PostPreviewDto } from '@/api';
import moment from 'moment';

interface IPostPreview {
  postPreview: PostPreviewDto;
}

const PostPreview: React.FC<IPostPreview> = ({ postPreview }) => {
  return (
    <Segment>
      <Link to={`/posts/${postPreview.id}`}>
        <h3>{postPreview.title}</h3>
      </Link>
      <p>
        <Tooltip title={moment(postPreview.date).format('YYYY-MM-DD HH:mm:ss')}>
          <span>Опубликовано {moment(postPreview.date).fromNow()}</span>
        </Tooltip>
        <br />
        пользователем {postPreview.username}
      </p>
      <Divider />
      <p>{postPreview.excerpt}</p>
      Теги: <Tag> {postPreview.tag}</Tag>
      <Divider />
      <Link to={`/posts/${postPreview.id}`}>
        <CommentIcon />
        {postPreview.comments}
      </Link>
    </Segment>
  );
};

export default PostPreview;
