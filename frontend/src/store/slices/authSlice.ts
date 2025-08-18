import { createSlice, PayloadAction } from '@reduxjs/toolkit';

type UserRole = 'admin' | 'user';

interface AuthState {
  token: string | null;
  role: UserRole | null;
}

const initialState: AuthState = {
  token: localStorage.getItem('token'),
  role: localStorage.getItem('role') as UserRole | null,
};

export const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    setCredentials: (state, action: PayloadAction<{ token: string; role: UserRole }>) => {
      state.token = action.payload.token;
      state.role = action.payload.role;
      localStorage.setItem('token', action.payload.token);
      localStorage.setItem('role', action.payload.role);
    },
    logout: {
      reducer: (state) => {
        state.token = null;
        state.role = null;
      },
      prepare: () => {
        localStorage.removeItem('token');
        localStorage.removeItem('role');
        return { payload: undefined };
      },
    },
  },
});

export const { setCredentials, logout } = authSlice.actions;
export default authSlice.reducer;