import { Button, Col, Row } from 'antd';
import React from 'react';
import Router from 'umi/router';

const Index: React.FunctionComponent = () => {
  const handleClick = () => {
    Router.push('/posts');
  };

  return (
    <div>
      <Row>
        <Col span={12}>
          <h1>Дневник снов</h1>
        </Col>
      </Row>
      <Row>
        <Col span={12}>
          <h3>Идеальное место для ведения личного дневника сноведений</h3>
        </Col>
      </Row>
      <Row>
        <Col span={12}>
          <Button htmlType={'button'} onClick={handleClick}>
            Перейти на страницу с постами
          </Button>
        </Col>
      </Row>
    </div>
  );
};

export default Index;
