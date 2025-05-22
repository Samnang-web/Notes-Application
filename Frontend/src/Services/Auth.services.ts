
import axios from './api';
export const login = async (username: string, password: string) => {
  const response = await axios.post('/Auth/login', { username, password });
  return response.data;
};

export const register = async (username: string, password: string) => {
  const response = await axios.post('/Auth/register', { username, password });
  return response.data;
};
