import { Spin } from 'antd';
import React from 'react';
import { RouteComponentProps } from 'react-router';
import withRouter from 'umi/withRouter';
import useAuth from '@/hooks/useAuth';
import styled from 'styled-components';

const SpinContainer = styled.div`
  position: absolute;
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100vh;
  width: 100vw;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background-color: white;
`;

const Callback: React.FC<RouteComponentProps> = props => {
  const auth = useAuth();
  const handleAuthentication = async (path: string) => {
    if (/access_token|id_token|error/.test(path)) {
      await auth.handleAuthentication();
    }
  };
  handleAuthentication(props.location.hash);
  return (
    <SpinContainer>
      <Spin size={'large'} />
    </SpinContainer>
  );
};

export default withRouter(Callback);
