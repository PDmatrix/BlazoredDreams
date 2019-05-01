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
}

const CommentList: React.FC<ICommentList> = ({
  comments,
  addComment,
  deleteComment,
  editComment,
}) => {
  const auth = useAuth();

  const formattedComments = comments.map(comment => {
    return {
      actions: [<span>Reply to</span>],
      author: comment.username,
      avatar: 'https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png',
      content: <p>{comment.content}</p>,
      datetime: (
        <Tooltip
          title={moment()
            .subtract(1, 'days')
            .format('YYYY-MM-DD HH:mm:ss')}
        >
          <span>
            {moment()
              .subtract(1, 'days')
              .fromNow()}
          </span>
        </Tooltip>
      ),
    };
  });

  return (
    <Segment>
      <List
        header={`${formattedComments.length} комментария`}
        itemLayout="horizontal"
        dataSource={formattedComments}
        renderItem={item => (
          <Comment
            actions={item.actions}
            author={item.author}
            avatar={item.avatar}
            content={item.content}
            datetime={item.datetime}
          />
        )}
      />
      {/*      {comments.map(comment => (
        <Comment
          editComment={editComment}
          deleteComment={deleteComment}
          key={comment.id}
          comment={comment}
        />
      ))}*/}
      {auth.isAuthenticated() && <CommentInput addComment={addComment} />}
    </Segment>
  );
};

export default CommentList;
