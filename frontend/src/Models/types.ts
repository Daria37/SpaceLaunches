// Типы для запусков
export interface Launch {
  id: string;
  name: string;
  date: string;
  rocket: string;
  agency?: string;
  country?: string;
}

// Типы для пользователей (админка)
export interface User {
  id: string;
  email: string;
  role: 'admin' | 'user';
}

// Типы для Redux
export interface AuthState {
  token: string | null;
  role: 'admin' | 'user' | null;
}

export interface LaunchesState {
  launches: Launch[];
  loading: boolean;
  error: string | null;
}