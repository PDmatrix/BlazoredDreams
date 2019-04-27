import { Button, Divider, Input, Modal } from 'antd';
import React from 'react';
import Auth from '../../lib/Auth';
import styles from './styles.css';

interface IUserInfoInterface {
  name: string;
  email: string;
}

const UserInfo: React.FunctionComponent<IUserInfoInterface> = props => {
  const [modal, setModal] = React.useState(false);

  const handleClick = () => {
    Auth.getInstance().logout();
  };

  const handleModal = () => {
    setModal(!modal);
  };

  return (
    <>
      <h4>Имя пользователя:</h4>
      <span className={styles.userData}>{props.name}</span>
      <h4>Электронная почта:</h4>
      <span className={styles.userData}>{props.email}</span>
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
