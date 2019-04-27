import { notification } from 'antd';

const useNotification = () => {
  return {
    success: (message: string, description?: string) =>
      notification.success({ message: message, description: description }),
    error: (message: string, description?: string) =>
      notification.error({ message: message, description: description }),
    info: (message: string, description?: string) =>
      notification.info({ message: message, description: description }),
  };
};

export default useNotification;
