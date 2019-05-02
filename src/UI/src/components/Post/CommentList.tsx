import React from 'react';
import { CommentDto, CommentRequest } from '@/api';
/*import Comment from './Comment';*/
import CommentInput from './CommentInput';
import { Segment } from '@/components/Shared/Segment';
import useAuth from '@/hooks/useAuth';
import moment from 'moment';
import { List, Tooltip, Comment } from 'antd';

interface ICommentList {
  comments: CommentDto[];
  addComment: (commentRequest: CommentRequest) => Promise<void>;
  deleteComment: (commentId: number) => Promise<void>;
  editComment: (commentId: number, commentRequest: CommentRequest) => Promise<void>;
  avatar: string;
}

const CommentList: React.FC<ICommentList> = ({
  comments,
  addComment,
  deleteComment,
  editComment,
  avatar,
}) => {
  const auth = useAuth();

  const formattedComments = comments.map(comment => {
    return {
      author: comment.username,
      avatar: comment.avatar,
      content: <p>{comment.content}</p>,
      datetime: (
        <Tooltip title={moment(comment.date).format('YYYY-MM-DD HH:mm:ss')}>
          <span>{moment(comment.date).fromNow()}</span>
        </Tooltip>
      ),
    };
  });

  return (
    <Segment>
      {comments.length > 0 && (
        <List
          header={`${formattedComments.length} комментария`}
          itemLayout="horizontal"
          dataSource={formattedComments}
          renderItem={item => (
            <Comment
              author={item.author}
              avatar={item.avatar}
              content={item.content}
              datetime={item.datetime}
            />
          )}
        />
      )}
      {auth.isAuthenticated() && <CommentInput avatar={avatar} addComment={addComment} />}
    </Segment>
  );
};

export default CommentList;
