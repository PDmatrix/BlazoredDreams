import { Divider } from 'antd';
import moment from 'moment';
import React from 'react';

const Comment: React.FunctionComponent = () => {
  return <div>Comment</div>;
  /*return (
    <div>
      <h4>{props.username}</h4>
      <span>
        {moment(props.date_created)
          .locale('ru')
          .format('L LT')}{' '}
      </span>
      <p>{props.content}</p>
      <Divider />
    </div>
  );*/
};

export default Comment;
