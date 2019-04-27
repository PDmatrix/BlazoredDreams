import React from 'react';
import useAuth from '@/hooks/useAuth';

const PrivateRoute: React.FC = props => {
  const auth = useAuth();
  if (auth.isAuthenticated()) return <>{props.children}</>;
  auth.login();
  return null;
};

export default PrivateRoute;
