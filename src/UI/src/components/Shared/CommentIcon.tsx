import { Icon } from 'antd';
import React from 'react';
import styles from './styles.css';

const CommentIcon: React.FC = () => {
  return <Icon type="message" theme={'outlined'} className={styles.commentIcon} />;
};

export default CommentIcon;
