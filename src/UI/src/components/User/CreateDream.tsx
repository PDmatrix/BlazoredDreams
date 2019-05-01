import { Button, DatePicker, Input } from 'antd';
import React, { useState } from 'react';
import { DreamRequest } from '@/api';
import { Moment } from 'moment';

interface ICreateDream {
  createDream: (dreamRequest: DreamRequest) => Promise<void>;
}

const CreateDream: React.FC<ICreateDream> = ({ createDream }) => {
  const handleChange = (e: any) => {
    setInput(e.currentTarget.value);
  };

  const handleDateChange = (date: Moment) => {
    setInputDate(date.toDate());
  };

  const handleClick = async () => {
    setInput('');
    await createDream({ content: input, date: inputDate });
  };

  const [input, setInput] = useState('');
  const [inputDate, setInputDate] = useState(new Date());
  return (
    <>
      <h3>Добавить сон:</h3>
      <DatePicker
        showTime={true}
        format="YYYY-MM-DD HH:mm:ss"
        placeholder={'Время сна'}
        onChange={handleDateChange}
      />
      <br />
      <br />
      <Input.TextArea
        autosize={{ minRows: 6, maxRows: 10 }}
        onChange={handleChange}
        value={input}
      />
      <br />
      <br />
      <Button onClick={handleClick} htmlType={'button'} type="primary">
        Добавить сон
      </Button>
    </>
  );
};

export default CreateDream;
