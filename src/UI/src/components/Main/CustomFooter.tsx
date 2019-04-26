import { Layout } from 'antd';
import React from 'react';
import styled from 'styled-components';

const Footer = styled(Layout.Footer)`
  text-align: center;
`;

const CustomFooter: React.FC<{ text: string }> = ({ text }) => {
  return (
    <Footer>
      <span>{text}</span>
    </Footer>
  );
};

export default CustomFooter;
