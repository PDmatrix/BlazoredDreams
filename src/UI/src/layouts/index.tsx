import { Layout, LocaleProvider } from 'antd';
import ru_RU from 'antd/lib/locale-provider/ru_RU';
import React from 'react';
import CustomContent from '../components/Main/CustomContent';
import CustomHeader from '../components/Main/CustomHeader';
import styles from './index.css';

const BaseLayout: React.FunctionComponent = props => {
  return (
    <LocaleProvider locale={ru_RU}>
      <Layout>
        <CustomHeader />
        <div className={styles.content}>
          <CustomContent>{props.children}</CustomContent>
        </div>
      </Layout>
    </LocaleProvider>
  );
};

export default BaseLayout;
