import { Divider } from 'antd';
import React from 'react';
import { CommentDto } from '@/api';

interface IComment {
  comment: CommentDto;
}

const Comment: React.FunctionComponent<IComment> = ({ comment }) => {
  return (
    <div>
      <h4>{comment.username}</h4>
      <span>{comment.date}</span>
      <p>{comment.content}</p>
      <Divider />
    </div>
  );
};

export default Comment;
