import React from 'react';
import { CommentDto, CommentRequest } from '@/api';
import Comment from './Comment';
import CommentInput from './CommentInput';
import { Segment } from '@/components/Shared/Segment';
import useAuth from '@/hooks/useAuth';

interface ICommentList {
  comments: CommentDto[];
  addComment: (commentRequest: CommentRequest) => Promise<void>;
}

const CommentList: React.FC<ICommentList> = ({ comments, addComment }) => {
  const auth = useAuth();

  return (
    <Segment>
      {comments.map(comment => (
        <Comment key={comment.id} comment={comment} />
      ))}
      {auth.isAuthenticated() && <CommentInput addComment={addComment} />}
    </Segment>
  );
};

export default CommentList;
