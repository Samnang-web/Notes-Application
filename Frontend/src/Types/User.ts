export interface User {
  id: number;
  username: string;
  email: string;
  password?: string; // optional when returning from server
  createdAt?: string;
}
