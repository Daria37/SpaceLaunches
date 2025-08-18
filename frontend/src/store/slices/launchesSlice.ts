import { createSlice, createAsyncThunk, PayloadAction } from '@reduxjs/toolkit';
import { api } from '../../Services/api';
import { Launch } from '../../Models/types';
import { RootState } from '../store';

interface LaunchesState {
  launches: Launch[];
  stats: {
    byYear: Launch[];
    byCountry: Launch[];
  };
  loading: boolean;
  error: string | null;
  filters: {
    year?: string;
    agency?: string;
  };
}

const initialState: LaunchesState = {
  launches: [],
  stats: {
    byYear: [],
    byCountry: [],
  },
  loading: false,
  error: null,
  filters: {},
};

// Асинхронные Thunk-запросы
export const fetchLaunches = createAsyncThunk(
  'launches/fetchLaunches',
  async (_, { getState }) => {
    const state = getState() as RootState;
    const { year, agency } = state.launches.filters;
    const response = await api.get('/launches', {
      params: { year, agency },
    });
    return response.data;
  }
);

export const fetchLaunchesStats = createAsyncThunk(
  'launches/fetchStats',
  async (type: 'byYear' | 'byCountry') => {
    const response = await api.get(`/launches/stats?type=${type}`);
    return { type, data: response.data };
  }
);

const launchesSlice = createSlice({
  name: 'launches',
  initialState,
  reducers: {
    setFilters: (
      state,
      action: PayloadAction<{ year?: string; agency?: string }>
    ) => {
      state.filters = action.payload;
    },
    resetFilters: (state) => {
      state.filters = {};
    },
  },
  extraReducers: (builder) => {
    builder
      // Загрузка запусков
      .addCase(fetchLaunches.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchLaunches.fulfilled, (state, action) => {
        state.launches = action.payload;
        state.loading = false;
      })
      .addCase(fetchLaunches.rejected, (state, action) => {
        state.error = action.error.message || 'Failed to fetch launches';
        state.loading = false;
      })
      // Статистика
      .addCase(fetchLaunchesStats.fulfilled, (state, action) => {
        state.stats[action.payload.type] = action.payload.data;
      });
  },
});

export const { setFilters, resetFilters } = launchesSlice.actions;
export default launchesSlice.reducer;