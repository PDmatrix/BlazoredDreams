import React from 'react';
import { Segment } from '../Shared/Segment';
import Dream from './Dream';
import { DreamDto, PostRequest } from '@/api';

interface IDreamList {
  dreams: DreamDto[];
  deleteDream: (id: number) => Promise<void>;
  createPost: (postRequest: PostRequest) => Promise<void>;
}

const DreamList: React.FC<IDreamList> = ({ dreams, deleteDream, createPost }) => {
  return (
    <Segment>
      {dreams.map(dream => (
        <Dream createPost={createPost} deleteDream={deleteDream} key={dream.id} dream={dream} />
      ))}
    </Segment>
  );
};

export default DreamList;
