import React from 'react';
import { PostDto } from '@/api';
import { Divider, Tag, Tooltip, Typography, Col, Row, Button, Upload, Avatar } from 'antd';
import { Segment } from '@/components/Shared/Segment';
import moment from 'moment';
import useAuth from '@/hooks/useAuth';

interface IPost {
  post: PostDto;
  changeUserImage: (postId: number) => any;
}

const renderAlone = (post: PostDto) => (
  <Typography>
    <Typography.Paragraph>{post.content}</Typography.Paragraph>
  </Typography>
);

const renderWithImage = (post: PostDto) => (
  <Row>
    <Col span={12}>
      <Typography>
        <Typography.Paragraph>{post.content}</Typography.Paragraph>
      </Typography>
    </Col>
    <Col span={12}>
      <img width={'100%'} src={post.cover} alt={'Cover'} />
    </Col>
  </Row>
);

const Post: React.FC<IPost> = ({ post, changeUserImage }) => {
  const auth = useAuth();
  return (
    <Segment>
      <Typography>
        <Typography.Title level={4}>{post.title}</Typography.Title>
        {post.username === auth.getUserData().nickname && !Boolean(post.cover) && (
          <Upload name="file" onChange={changeUserImage(post.id)} showUploadList={false}>
            <Button>Добавить обложку</Button>
          </Upload>
        )}
        <Divider />
      </Typography>
      {Boolean(post.cover) && renderWithImage(post)}
      {!Boolean(post.cover) && renderAlone(post)}
      <Typography>
        <Divider />
        Теги:{' '}
        {post.tag.split(',').map((tag, idx) => {
          return <Tag key={idx}>{tag}</Tag>;
        })}
        <Typography.Paragraph>
          <Tooltip title={moment(post.date).format('YYYY-MM-DD HH:mm:ss')}>
            <span>Опубликовано {moment(post.date).fromNow()}</span>
          </Tooltip>
          <br />
          пользователем {post.username}
        </Typography.Paragraph>
      </Typography>
    </Segment>
  );
};

export default Post;
