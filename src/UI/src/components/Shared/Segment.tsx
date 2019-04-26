import React from 'react';
import styles from './styles.css';

export const Segment: React.FunctionComponent = ({ children }) => {
  return <div className={styles.segment}>{children}</div>;
};
