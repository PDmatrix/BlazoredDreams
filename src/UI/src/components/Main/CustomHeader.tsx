import { Layout } from 'antd';
import React from 'react';
import Navigation from './Navigation';
import styled from 'styled-components';
import logo from '@/assets/logo.png';

const Header = styled(Layout.Header)`
  background-color: #fff;
  box-shadow: 0 3px 6px rgba(0, 0, 0, 0.16), 0 3px 6px rgba(0, 0, 0, 0.23);
`;

const Title = styled.span`
  font-weight: bold;
  float: right;

  @media screen and (max-width: 880px) {
    display: none;
  }
`;

const CustomHeader: React.FC = () => {
  return (
    <Header>
      <Title>
        <img alt="logo" src={logo} />
      </Title>
      <Navigation />
    </Header>
  );
};

export default CustomHeader;
