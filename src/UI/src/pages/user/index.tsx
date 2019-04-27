import { Button, Divider, Spin } from 'antd';
import React, { useState } from 'react';
import useAuth from '@/hooks/useAuth';

const User: React.FunctionComponent = () => {
  const auth = useAuth();
  return (
    <div>
      User <Button onClick={() => auth.login()}>Login</Button>
      <Button onClick={() => auth.logout()}>Logout</Button>
    </div>
  );
};

export default User;
