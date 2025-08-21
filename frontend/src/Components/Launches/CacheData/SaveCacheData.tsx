import { ISpaceLaunch } from "../../../spacelaunches";

const CACHE_KEY = 'space_launches_cache';
const CACHE_DURATION = 24 * 60 * 60 * 1000; // 24 часа

const saveToCache = (data: ISpaceLaunch[]) => {
  try {
    localStorage.setItem(CACHE_KEY, JSON.stringify({
      data,
      timestamp: Date.now()
    }));
  } catch (error) {
    console.warn('Failed to save cache:', error);
  }
};

export default saveToCache;