
import axios from './api';

export const login = async (email: string, password: string) => {
  const response = await axios.post('/auth/login', { email, password });
  return response.data;
};

export const register = async (username: string, email: string, password: string) => {
  const response = await axios.post('/auth/register', { username, email, password });
  return response.data;
};
