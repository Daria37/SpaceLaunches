import { ISpaceLaunch } from "../../../spacelaunches";

const CACHE_KEY = 'space_launches_cache';
const CACHE_DURATION = 24 * 60 * 60 * 1000; // 24 часа

const getCachedData = (): ISpaceLaunch[] | null => {
  try {
    const cached = localStorage.getItem(CACHE_KEY);
    if (!cached) return null;

    const { data, timestamp } = JSON.parse(cached);
    
    // Проверяем не устарели ли данные
    if (Date.now() - timestamp < CACHE_DURATION) {
      return data;
    }
  } catch (error) {
    console.warn('Failed to read cache:', error);
  }
  return null;
};

export default getCachedData;