import axios from "axios"
import { CompanyProfile, ISpaceLaunch, ISpaceLaunchesResponse } from "./spacelaunches";
import getCachedData from "./Components/Launches/CacheData/GetCacheData";
import saveToCache from "./Components/Launches/CacheData/SaveCacheData";

export const searchLaunches = async (search: string): Promise<ISpaceLaunch[]> => {
  try {
    const response = await axios.get(
      `https://ll.thespacedevs.com/2.2.0//launches/?search=${search}`
    );
    return response.data.results;
  } catch (error) {
    console.error("Error fetching launches:", error);
    return [];
  }
};

// export const getAllLaunches = async (): Promise<ISpaceLaunch[]> => {
//   // Пробуем взять данные из кэша
//   const cachedData = getCachedData();
//   if (cachedData && cachedData.length > 1000) { // Используем кэш только если там много данных
//     console.log('Using cached launch data:', cachedData.length, 'launches');
//     return cachedData;
//   }

//   console.log('Fetching fresh launch data...');
//   let allLaunches: ISpaceLaunch[] = [];
//   let nextUrl: string | null = `https://ll.thespacedevs.com/2.2.0/launch/?format=json&limit=100`;
//   let page = 1;

//   try {
//     while (nextUrl && page <= 40) { // Ограничим 20 страницами (2000 запусков)
//       console.log(`Fetching page ${page}...`);
      
//       const response = await axios.get(`https://ll.thespacedevs.com/2.2.0/launch/?format=json&limit=100`);
//       allLaunches = [...allLaunches, ...response.data.results];
//       nextUrl = response.data.next;
//       page++;

//       // Задержка чтобы не перегружать API
//       await new Promise(resolve => setTimeout(resolve, 200));
      
//       // Сохраняем промежуточные результаты в кэш
//       saveToCache(allLaunches);
//     }

//     console.log(`Fetched ${allLaunches.length} launches total`);
//     return allLaunches;
//   } catch (error) {
//     console.error("Error fetching launches:", error);
    
//     // Если есть какие-то данные в кэше, используем их
//     const fallbackData = getCachedData();
//     if (fallbackData) {
//       console.log('Using cached data as fallback');
//       return fallbackData;
//     }
    
//     throw new Error("Failed to fetch launch data");
//   }
// };

const getLaunchesForYear = async (year: number): Promise<ISpaceLaunch[]> => {
  try {
    const response = await axios.get(
      `https://ll.thespacedevs.com/2.2.0/launch/?format=json&limit=100&window_start__gte=${year}-01-01&window_start__lte=${year}-12-31`
    );
    console.log(`Year ${year}: ${response.data.results.length} launches`);
    return response.data.results;
  } catch (error) {
    console.warn(`Failed to fetch data for year ${year}:`, error);
    return [];
  }
};

// export const getAllLaunches = async (): Promise<ISpaceLaunch[]> => {
//   // Пробуем взять данные из кэша
//   // const cachedData = getCachedData();
//   // if (cachedData && cachedData.length > 500) {
//   //   console.log('Using cached launch data:', cachedData.length, 'launches');
//   //   return cachedData;
//   // }

//   console.log('Fetching fresh launch data by years...');
  
//   try {
//     const yearsToFetch = [
//       // 1950-1960s
//       1957, 1958, 1959, 1960, 1961, 1962, 1963, 1964, 1965,
//       1966, 1967, 1968, 1969,
//       // 1970s
//       1970, 1971, 1972, 1973, 1974, 1975, 1976, 1977, 1978, 1979,
//       // 1980s
//       1980, 1981, 1982, 1983, 1984, 1985, 1986, 1987, 1988, 1989,
//       // 1990s
//       1990, 1991, 1992, 1993, 1994, 1995, 1996, 1997, 1998, 1999,
//       // 2000s
//       2000, 2001, 2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009,
//       // 2010s
//       2010, 2011, 2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019,
//       // 2020s
//       2020, 2021, 2022, 2023, 2024, 2025
//     ];

//     let allLaunches: ISpaceLaunch[] = [];
    
//     // Загружаем данные по каждому году
//     for (const year of yearsToFetch) {
//       const yearLaunches = await getLaunchesForYear(year);
//       allLaunches = [...allLaunches, ...yearLaunches];
      
//       // Сохраняем в кэш после каждого года
//       saveToCache(allLaunches);
      
//       // Пауза между запросами
//       await new Promise(resolve => setTimeout(resolve, 5000));
//     }

//     console.log(`Total fetched: ${allLaunches.length} launches`);
//     return allLaunches;
    
//   } catch (error) {
//     console.error("Error fetching launches by years:", error);
    
//     // Запасной вариант: старая пагинация
//     try {
//       console.log('Trying pagination method...');
//       return await getAllLaunchesFallback();
//     } catch (fallbackError) {
//       const fallbackData = getCachedData();
//       if (fallbackData) {
//         console.log('Using cached data as fallback');
//         return fallbackData;
//       }
//       throw new Error("Failed to fetch launch data");
//     }
//   }
// };

// export const getAllLaunchesFallback = async (): Promise<ISpaceLaunch[]> => {
//   try {
//     const response = await axios.get(
//       `https://ll.thespacedevs.com/2.2.0/launch/?format=json&limit=100`
//     );

//       // Добавляем задержку чтобы не спамить API
//       await new Promise(resolve => setTimeout(resolve, 100));
//       return response.data.results;
//   } catch (error) {
//     console.error("Error fetching launches:", error);
//     throw new Error("Failed to fetch launch data");
//   }
// };


export const fetchLaunches = async (limit: number = 10): Promise<ISpaceLaunch[]> => {
  try {
    const response = await axios.get(
      `https://ll.thespacedevs.com/2.2.0/launch/?format=json&limit=${limit}`
    );
    
    // Проверяем структуру ответа
    if (response.data && Array.isArray(response.data.results)) {
      return response.data.results;
    } else {
      return []
    }
    
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error("API Error:", error.response?.status, error.message);
      return [];
    }
    console.error("Unexpected error:", error);
    return [];
  }
};