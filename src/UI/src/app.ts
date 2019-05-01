import useNotification from '@/hooks/useNotification';
import axios, { AxiosError } from 'axios';
import useAuth from '@/hooks/useAuth';

const render = (oldRender: any) => {
  const notification = useNotification();
  const auth = useAuth();
  axios.interceptors.response.use(
    response => {
      return response;
    },
    (error: AxiosError) => {
      if (!error.response) return Promise.reject(error);
      if (error.response.status === 401) auth.login();
      notification.error(error.response.statusText);
      return Promise.reject(error);
    },
  );
  oldRender();
};

export { render };
