import axios from 'axios';

const API_URL = 'http://localhost:8080//swagger/index.html'; // Ваш C# бэкенд

export const api = axios.create({
  baseURL: API_URL,
  withCredentials: true,
});

// Добавляем JWT-токен к запросам
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});