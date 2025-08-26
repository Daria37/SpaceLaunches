# SpaceLaunches 🚀

[![React](https://img.shields.io/badge/React-18.x-%2361DAFB?logo=react)](https://reactjs.org/)
[![TypeScript](https://img.shields.io/badge/TypeScript-5.x-%233178C6?logo=typescript)](https://www.typescriptlang.org/)
[![Node.js](https://img.shields.io/badge/Node.js-20.x-%23339933?logo=nodedotjs)](https://nodejs.org/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16.x-%234169E1?logo=postgresql)](https://www.postgresql.org/)
[![Redis](https://img.shields.io/badge/Redis-7.x-%23DC382D?logo=redis)](https://redis.io/)
[![Mantine](https://img.shields.io/badge/Mantine-7.x-%2340C057?logo=mantine)](https://mantine.dev/)

Веб-приложение для отслеживания и визуализации данных о космических запусках. Этот дашборд предоставляет информацию о запусках, ракетах и космических агентствах, оснащен системой фильтрации, интерактивными графиками и системой ролевого доступа.

## ✨ Возможности

### 🚀 Основной функционал
- **База данных запусков**: Просмотр полного списка исторических космических запусков
- **Связанные сущности**: Детальная информация о каждом запуске, его ракете и соответствующем агентстве
- **Фильтрация**: Фильтрация по статусу запуска
- **Визуализация данных**:
  - График запусков по годам (столбчатая диаграмма)
  - График запусков по странам (круговая диаграмма)

### 👨‍💻 Пользовательская система и безопасность
- **JWT аутентификация**: Безопасный вход и управление сессиями
- **Ролевая модель доступа (RBAC)**:
  - **Admin**: Полный доступ ко всем данным и управлению пользователями (CRUD операции)
  - **User**: Ограниченный доступ (просмотр запусков после 2020 года и доступ к конкретному агентству)

### ⚡ Производительность
- **Кеширование в Redis**: Интеллектуальное кеширование API-ответов для ускорения загрузки
- **Оптимизированный бэкенд**: Эффективная обработка данных с использованием Node.js и Express

## 🛠️ Технологический стек

### Frontend
- **Фреймворк**: React 18 с TypeScript
- **Управление состоянием**: Redux Toolkit (RTK)
- **UI Библиотека**: Mantine UI
- **HTTP-клиент**: Axios
- **Графики**: Recharts / Chart.js

### Backend
- **Среда выполнения**: C#, TypeScript + React.js
- **Аутентификация**: JWT
- **База данных**: PostgreSQL
- **Кеширование**: Redis
- **Источник данных**: [Lloyd Launch Library (LLL) API](https://thespacedevs.com/lll)

## 📦 Установка и запуск

### Предварительные требования
- Node.js 18+
- PostgreSQL
- Redis
- npm или yarn

### 1. Клонирование репозитория
```bash
git clone https://github.com/Daria37/SpaceLaunches.git
cd SpaceLaunches