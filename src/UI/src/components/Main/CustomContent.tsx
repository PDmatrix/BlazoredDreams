import { Layout } from 'antd';
import React from 'react';
import styled from 'styled-components';

const Content = styled(Layout.Content)`
  padding: 20px 50px;
`;

const InnerContent = styled.div`
  padding: 24px;
  background: #fff;
  border-radius: 20px;
  min-height: 280px;
`;

const CustomContent: React.FC = ({ children }) => {
  return (
    <Content>
      <InnerContent>{children}</InnerContent>
    </Content>
  );
};

export default CustomContent;
