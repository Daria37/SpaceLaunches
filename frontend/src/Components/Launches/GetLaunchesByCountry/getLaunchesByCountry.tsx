import { ISpaceLaunch } from "../../../spacelaunches";

export const getLaunchesByCountry = (launches: ISpaceLaunch[]) => {
  const countryCounts: { [key: string]: number } = {};

  launches.forEach(launch => {
    const country = launch.pad.location.country_code || "Unknown";
    countryCounts[country] = (countryCounts[country] || 0) + 1;
  });

  const colors = ["#3B82F6", "#EF4444", "#10B981", "#8B5CF6", "#6366F1", "#F59E0B", "#EC4899"];
  
  return Object.entries(countryCounts)
    .sort(([, a], [, b]) => b - a)
    .slice(0, 10) // Топ-10 стран
    .map(([country, launches], index) => ({
      country,
      launches,
      color: colors[index % colors.length]
    }));
};

export default getLaunchesByCountry;