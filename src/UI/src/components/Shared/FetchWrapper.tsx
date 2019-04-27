import React from 'react';
import { Spin } from 'antd';

interface IFetchWrapper {
  isLoading: boolean;
}

const FetchWrapper: React.FC<IFetchWrapper> = props => {
  return <>{props.isLoading ? <Spin /> : props.children}</>;
};

export default FetchWrapper;
