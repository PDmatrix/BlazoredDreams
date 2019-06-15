import { Button, Divider, Input, Modal, Popconfirm, Typography } from 'antd';
import React, { useState } from 'react';
import { DreamDto, DreamRequest, PostRequest } from '@/api';
import useNotification from '@/hooks/useNotification';

const { Title, Paragraph } = Typography;

interface IDream {
  dream: DreamDto;
  deleteDream: (id: number) => Promise<void>;
  createPost: (postRequest: PostRequest) => Promise<void>;
  editDream: (id: number, content: string) => Promise<void>;
}

const Dream: React.FC<IDream> = ({ dream, deleteDream, createPost, editDream }) => {
  const notification = useNotification();
  const handleDelete = async () => {
    await deleteDream(dream.id);
  };

  const handlePublish = async () => {
    if (excerpt && title) {
      setModal(false);
      setExcerpt('');
      setTitle('');
      await createPost({ dreamId: dream.id, excerpt: excerpt, title: title, tags: tags });
    } else {
      notification.error('Не заполнены необходимые поля!');
    }
  };

  const changeTitle = (e: any) => {
    setTitle(e.currentTarget.value);
  };

  const changeExcerpt = (e: any) => {
    setExcerpt(e.currentTarget.value);
  };

  const changeTags = (e: any) => {
    setTags(e.currentTarget.value);
  };

  const showModal = () => {
    setModal(true);
  };

  const hideModal = () => {
    setModal(false);
  };

  const [modal, setModal] = useState(false);
  const [title, setTitle] = useState('');
  const [excerpt, setExcerpt] = useState('');
  const [tags, setTags] = useState('');

  return (
    <>
      <Paragraph editable={{ onChange: value => editDream(dream.id, value) }}>
        {dream.content}
      </Paragraph>
      <span>Дата: {dream.date}</span>
      <br />
      {!dream.isPublished && (
        <>
          <Button onClick={showModal} type={'primary'} htmlType={'button'}>
            Опубликовать сон
          </Button>
          <Divider type={'vertical'} />
          <Popconfirm
            title={'Вы действительно хотите удалить сон?'}
            onConfirm={handleDelete}
            okText={'Да'}
            cancelText={'Нет'}
          >
            <Button type={'danger'} htmlType={'button'}>
              Удалить сон
            </Button>
          </Popconfirm>
        </>
      )}
      <Divider />
      <Modal
        title="Введите заголовок записи, краткое описание, тэги"
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
        <br />
        <br />
        <p>Тэги:</p>
        <Input onKeyUp={changeTags} />
      </Modal>
    </>
  );
};

export default Dream;
