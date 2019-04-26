import { Icon } from 'antd';
import React from 'react';
import styles from './styles.css';

interface IHeartIconInterface {
  liked: boolean;
}

const HeartIcon: React.FunctionComponent<IHeartIconInterface> = ({ liked }) => {
  return <Icon type="heart" theme={liked ? 'filled' : 'outlined'} className={styles.heartIcon} />;
};

export default HeartIcon;
