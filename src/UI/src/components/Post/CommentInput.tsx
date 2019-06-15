import { Button, Input, Comment, Avatar } from 'antd';
import React, { useState } from 'react';
import styled from 'styled-components';
import { CommentRequest } from '@/api';
import useNotification from '@/hooks/useNotification';

const StyledButton = styled(Button)`
  margin-top: 10px;
`;

interface ICommentInput {
  addComment: (commentRequest: CommentRequest) => Promise<void>;
  avatar: string;
}

const CommentInput: React.FC<ICommentInput> = ({ addComment, avatar }) => {
  const notification = useNotification();
  const [input, setInput] = useState('');
  const handleChange = (e: any) => {
    setInput(e.currentTarget.value);
  };
  const handleClick = async () => {
    if (input) {
      setInput('');
      await addComment({ content: input });
    } else {
      notification.error('Не заполнены необходимые поля!');
    }
  };
  return (
    <>
      <Comment
        avatar={<Avatar src={avatar} />}
        content={
          <div>
            <Input.TextArea
              autosize={{ minRows: 6, maxRows: 10 }}
              onChange={handleChange}
              value={input}
            />
            <StyledButton onClick={handleClick} htmlType={'button'} type="primary">
              Добавить комментарий
            </StyledButton>
          </div>
        }
      />
    </>
  );
};

export default CommentInput;
