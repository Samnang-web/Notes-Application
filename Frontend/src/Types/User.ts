export interface User {
  id: number;
  username: string;
  password?: string; // optional when returning from server
  createdAt?: string;
}
