import React from 'react';
import styled from 'styled-components';

const SegmentContainer = styled.div`
  border: 1px solid rgba(34, 36, 38, 0.15);
  box-shadow: 0 1px 2px 0 rgba(34, 36, 38, 0.15);
  border-radius: 0.28571429rem;
  padding: 1em 1em;
  margin: 1em 1em;
`;

export const Segment: React.FC = ({ children }) => {
  return <SegmentContainer>{children}</SegmentContainer>;
};
