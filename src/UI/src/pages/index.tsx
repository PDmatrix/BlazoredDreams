import { Button, Col, Row } from 'antd';
import React from 'react';
import Link from 'umi/link';

const Index: React.FunctionComponent = () => {
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
          <Button htmlType={'button'}>
            <Link to={'/posts'}>Перейти на страницу с постами</Link>
          </Button>
        </Col>
      </Row>
    </div>
  );
};

export default Index;
