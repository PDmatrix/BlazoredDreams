import { Menu } from 'antd';
import React from 'react';
import { RouteComponentProps } from 'react-router';
import Link from 'umi/link';
import withRouter from 'umi/withRouter';
import styled from 'styled-components';

const StyledMenu = styled(Menu)`
  line-height: 64px;
`;

const Navigation: React.FC<RouteComponentProps> = props => {
  return (
    <StyledMenu theme="light" mode="horizontal" defaultSelectedKeys={[props.location.pathname]}>
      <Menu.Item key="/">
        <Link to="/">Главная страница</Link>
      </Menu.Item>
      <Menu.Item key="/posts">
        <Link to={'/posts'}>Публикации</Link>
      </Menu.Item>
      <Menu.Item key="/user">
        <Link to={`/user`}>Личная страница</Link>
      </Menu.Item>
    </StyledMenu>
  );
};

export default withRouter(Navigation);
