import React from 'react';
import { CommentDto, CommentRequest } from '@/api';
import CommentInput from './CommentInput';
import { Segment } from '@/components/Shared/Segment';
import useAuth from '@/hooks/useAuth';
import moment from 'moment';
import { List, Tooltip, Comment, Popconfirm, Typography } from 'antd';

const { Paragraph } = Typography;

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

  const deleteHandler = (e: any, d: any) => {
    console.log(e, d);
  };

  const formattedComments = comments.map(comment => {
    return {
      author: comment.username,
      avatar: comment.avatar,
      content:
        comment.userId === auth.getUserId()
          ? [
              <Paragraph
                editable={{ onChange: value => editComment(comment.id, { content: value }) }}
              >
                {comment.content}
              </Paragraph>,
            ]
          : [<Paragraph>{comment.content}</Paragraph>],
      datetime: (
        <Tooltip title={moment(comment.date).format('YYYY-MM-DD HH:mm:ss')}>
          <span>{moment(comment.date).fromNow()}</span>
        </Tooltip>
      ),
      actions:
        comment.userId === auth.getUserId()
          ? [
              <Popconfirm
                title={'Вы действительно хотите удалить комментарий?'}
                okText={'Да'}
                cancelText={'Нет'}
                onConfirm={() => deleteComment(comment.id)}
              >
                <span>Удалить</span>
              </Popconfirm>,
            ]
          : [],
    };
  });
  if (comments.length === 0 && !auth.isAuthenticated()) return null;
  return (
    <Segment>
      {comments.length > 0 && (
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
      )}
      {auth.isAuthenticated() && <CommentInput avatar={avatar} addComment={addComment} />}
    </Segment>
  );
};

export default CommentList;
