import { Icon } from 'antd';
import React from 'react';
import styled from 'styled-components';

const CommentIconContainer = styled(Icon)`
  color: #1890ff;
  padding: 0 5px;
  font-size: 16px;
  transition: all ease 0.4s;
`;

const CommentIcon: React.FC = () => {
  return <CommentIconContainer type="message" theme={'outlined'} />;
};

export default CommentIcon;
