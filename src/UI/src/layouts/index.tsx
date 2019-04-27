import { Layout, LocaleProvider, Alert } from 'antd';
import ru_RU from 'antd/lib/locale-provider/ru_RU';
import React from 'react';
import CustomContent from '../components/Main/CustomContent';
import CustomHeader from '../components/Main/CustomHeader';

const BaseLayout: React.FC = props => {
  return (
    <LocaleProvider locale={ru_RU}>
      <Layout>
        <CustomHeader />
        <CustomContent>{props.children}</CustomContent>
      </Layout>
    </LocaleProvider>
  );
};

export default BaseLayout;
