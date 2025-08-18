import { configureStore, ThunkAction } from '@reduxjs/toolkit';
import authReducer from './slices/authSlice';
import launchesReducer from './slices/launchesSlice';
import adminReducer from './slices/adminSlice';
import type { TypedUseSelectorHook } from 'react-redux';
import { useDispatch, useSelector } from 'react-redux';

export const store = configureStore({
  reducer: {
    auth: authReducer,
    launches: launchesReducer,
    admin: adminReducer,
  },
  middleware: (getDefaultMiddleware) => 
    getDefaultMiddleware({
      serializableCheck: false, // Для работы с Mantine
    }),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;
export const useAppDispatch: () => AppDispatch = useDispatch;