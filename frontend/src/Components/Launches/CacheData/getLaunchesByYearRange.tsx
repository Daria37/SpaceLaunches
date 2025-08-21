import axios from "axios";
import { ISpaceLaunch } from "../../../spacelaunches";
import saveToCache from "./SaveCacheData";

export const getLaunchesByYearRange = async (startYear: number, endYear: number): Promise<ISpaceLaunch[]> => {
  const results: ISpaceLaunch[] = [];
  console.log(`Fetching launches from ${startYear} to ${endYear}...`);
  
  for (let year = startYear; year <= endYear; year++) {
    try {
      const response = await axios.get(
        `https://ll.thespacedevs.com/2.2.0/launch/?format=json&limit=100&window_start__gte=${year}-01-01&window_start__lte=${year}-12-31`
      );
      
      results.push(...response.data.results);
      console.log(`Year ${year}: ${response.data.results.length} launches`);
      
      // Сохраняем в кэш после каждого года
      saveToCache(results);
      
      await new Promise(resolve => setTimeout(resolve, 150)); // Увеличили задержку
      
    } catch (error) {
      console.warn(`Failed to fetch data for year ${year}:`, error);
      // Продолжаем со следующим годом вместо прерывания
    }
  }
  
  return results;
};