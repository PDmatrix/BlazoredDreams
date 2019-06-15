import { Button, Divider, Modal, Avatar, Upload, Tooltip, Row, Col } from 'antd';
import React from 'react';
import styles from './styles.css';
import useAuth from '@/hooks/useAuth';

interface IUserInfoInterface {
  username: string;
  email: string;
  avatar: string;
  changeUserImage: (info: any) => Promise<void>;
}

const UserInfo: React.FC<IUserInfoInterface> = ({ username, email, avatar, changeUserImage }) => {
  const [modal, setModal] = React.useState(false);
  const auth = useAuth();
  const handleClick = () => {
    auth.logout();
  };

  const handleModal = () => {
    setModal(!modal);
  };
  return (
    <>
      <Upload name="file" onChange={changeUserImage} showUploadList={false}>
        <Tooltip placement={'right'} title="Кликните для изменения аватара">
          <Avatar src={avatar} size={256} />
        </Tooltip>
      </Upload>
      <h4>Имя пользователя:</h4>
      <span className={styles.userData}>{username}</span>
      <h4>Электронная почта:</h4>
      <span className={styles.userData}>{email}</span>
      <br />
      <Button type={'primary'} onClick={handleClick} htmlType={'button'}>
        Выйти
      </Button>
      <Divider type={'vertical'} />
      <Button type={'ghost'} onClick={handleModal} htmlType={'button'}>
        Руководство пользователя
      </Button>
      <Divider />
      <Modal
        title="Руководство пользователя"
        visible={modal}
        onOk={handleModal}
        onCancel={handleModal}
      >
        Для добавления описания сна, сначала необходимо выбрать дату сна.
        <hr />
        Далее, необходимо написать описание этого сна и после этого нажать на кнопку "Добавить сон".
        <hr />
        Далее, этот сон можно опубликовать на всеобщее обозрение, для этого нужно нажать кнопку
        "Опубликовать сон" рядом с описанием сна. Далее, необходимо ввести заголовок поста и нажать
        кнопку "ОК".
        <hr />
        Также, у пользователя есть возможность удалить описание сна, для этого нужно нажать кнопку
        "Удалить" рядом с описанием сна.
        <br />
        <strong>Внимание, данное действие необратимо!</strong>
      </Modal>
    </>
  );
};

export default UserInfo;
