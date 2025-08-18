import { api } from '../Services/api';
import { User } from '../Models/types';

export const login = async (email: string, password: string): Promise<{ token: string; user: User }> => {
  const response = await api.post('/auth/login', { email, password });
  return response.data;
};

export const getUsers = async (): Promise<User[]> => {
  const response = await api.get('/admin/users');
  return response.data;
};

export const logout = (): void => {
  localStorage.removeItem('token');
};