import { Divider, Icon, Typography } from 'antd';
import React from 'react';
import { CommentDto, CommentRequest } from '@/api';
import useAuth from '@/hooks/useAuth';

const { Paragraph, Text, Title } = Typography;

interface IComment {
  comment: CommentDto;
  deleteComment: (commentId: number) => Promise<void>;
  editComment: (commentId: number, commentRequest: CommentRequest) => Promise<void>;
}

const Comment: React.FC<IComment> = ({ comment, deleteComment, editComment }) => {
  const auth = useAuth();

  const handleDelete = async () => {
    await deleteComment(comment.id);
  };

  const handleChange = async (newContent: string) => {
    if (newContent !== comment.content) await editComment(comment.id, { content: newContent });
  };

  return (
    <Typography>
      <Title level={4}>{comment.username}</Title>
      <Text>{comment.date}</Text>
      <Paragraph ellipsis={{ expandable: true }} editable={{ onChange: handleChange }}>
        {comment.content}
      </Paragraph>
      {auth.getUserId() === comment.userId && (
        <Icon title="Удалить" onClick={handleDelete} type="delete" theme="twoTone" />
      )}
      <Divider />
    </Typography>
  );
};

export default Comment;
