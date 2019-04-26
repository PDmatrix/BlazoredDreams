import { Spin } from 'antd';
import React from 'react';
import { RouteComponentProps } from 'react-router';
import withRouter from 'umi/withRouter';
import styles from './callback.css';

const Callback: React.FC<RouteComponentProps> = props => {
  return (
    <div className={styles.callback}>
      <Spin size={'large'} />
    </div>
  );
};

export default withRouter(Callback);
