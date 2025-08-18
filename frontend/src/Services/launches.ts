import { api } from './api';

export const fetchLaunches = async () => {
  const response = await api.get('/launches');
  return response.data;
};