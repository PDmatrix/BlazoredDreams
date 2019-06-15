import React from 'react';
import { PageOfPostPreviewDto, PostRequest } from '@/api';
import PostPreview from '@/components/Page/PostPreview';

interface IPostList {
  posts: PageOfPostPreviewDto;
  editPost: (postId: number, postRequest: PostRequest) => Promise<void>;
}

const PostList: React.FC<IPostList> = props => {
  if (!props.posts.records) return null;
  return (
    <div>
      {props.posts.records.map(post => (
        <PostPreview editPost={props.editPost} postPreview={post} key={post.id} />
      ))}
    </div>
  );
};

export default PostList;
