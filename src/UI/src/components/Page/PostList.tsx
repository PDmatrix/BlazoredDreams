import React from 'react';
import { PageOfPostPreviewDto } from '@/api';
import PostPreview from '@/components/Page/PostPreview';

interface IPostList {
  posts: PageOfPostPreviewDto;
}

const PostList: React.FC<IPostList> = props => {
  if (!props.posts.records) return null;
  return (
    <div>
      {props.posts.records.map(post => (
        <PostPreview postPreview={post} key={post.id} />
      ))}
    </div>
  );
};

export default PostList;
