import { Button, Divider, Input, Modal } from 'antd';
import React, { useState } from 'react';
import { DreamDto, PostRequest } from '@/api';

interface IDream {
  dream: DreamDto;
  deleteDream: (id: number) => Promise<void>;
  createPost: (postRequest: PostRequest) => Promise<void>;
}

const Dream: React.FC<IDream> = ({ dream, deleteDream, createPost }) => {
  const handleDelete = async () => {
    await deleteDream(dream.id);
  };

  const handlePublish = async () => {
    setModal(false);
    setExcerpt('');
    setTitle('');
    await createPost({ dreamId: dream.id, excerpt: except, title: title });
  };

  const changeTitle = (e: any) => {
    setTitle(e.currentTarget.value);
  };

  const changeExcerpt = (e: any) => {
    setExcerpt(e.currentTarget.value);
  };

  const showModal = () => {
    setModal(true);
  };

  const hideModal = () => {
    setModal(false);
  };

  const [modal, setModal] = useState(false);
  const [title, setTitle] = useState('');
  const [except, setExcerpt] = useState('');

  return (
    <>
      <h3>{dream.content}</h3>
      <span>Дата: {dream.date}</span>
      <br />
      {!dream.isPublished && (
        <>
          <Button onClick={showModal} type={'primary'} htmlType={'button'}>
            Опубликовать сон
          </Button>
          <Divider type={'vertical'} />
          <Button onClick={handleDelete} type={'danger'} htmlType={'button'}>
            Удалить сон
          </Button>
        </>
      )}
      <Divider />
      <Modal
        title="Введите заголовок записи и краткое описание"
        visible={modal}
        onOk={handlePublish}
        onCancel={hideModal}
      >
        <p>Заголовок:</p>
        <Input onKeyUp={changeTitle} />
        <br />
        <br />
        <p>Краткое описание:</p>
        <Input onKeyUp={changeExcerpt} />
      </Modal>
    </>
  );
};

export default Dream;
