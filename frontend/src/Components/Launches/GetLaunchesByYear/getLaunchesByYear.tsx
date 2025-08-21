import { ISpaceLaunch } from "../../../spacelaunches";

export const getLaunchesByYear = (launches: ISpaceLaunch[]) => {
  const yearCounts: { [key: number]: number } = {};

  launches.forEach(launch => {
    try {
      const year = new Date(launch.window_start).getFullYear();
      if (year >= 1957 && year <= 2025) {
        yearCounts[year] = (yearCounts[year] || 0) + 1;
      }
    } catch (error) {
      console.warn("Invalid date:", launch.window_start);
    }
  });

  // Создаем массив всех лет от 1957 до 2025
  const allYears = Array.from({ length: 2025 - 1957 + 1 }, (_, i) => 1957 + i);
  
  // Заполняем данные для всех лет
  const yearlyData = allYears.map(year => ({
    year: year.toString(),
    launches: yearCounts[year] || 0
  }));

  // Группируем по десятилетиям для лучшей визуализации
  const decades = [
    { range: "1957-1969", start: 1957, end: 1969 },
    { range: "1970-1979", start: 1970, end: 1979 },
    { range: "1980-1989", start: 1980, end: 1989 },
    { range: "1990-1999", start: 1990, end: 1999 },
    { range: "2000-2009", start: 2000, end: 2009 },
    { range: "2010-2019", start: 2010, end: 2019 },
    { range: "2020-2025", start: 2020, end: 2025 }
  ];

  return decades.map(decade => {
    const launchesInDecade = yearlyData
      .filter(item => {
        const year = parseInt(item.year);
        return year >= decade.start && year <= decade.end;
      })
      .reduce((sum, item) => sum + item.launches, 0);

    return {
      year: decade.range,
      launches: launchesInDecade
    };
  }).filter(item => item.launches > 0);
};

// export const getLaunchesByYear = (launches: ISpaceLaunch[]) => {
//   const yearCounts: { [key: string]: number } = {};

//   launches.forEach(launch => {
//     try {
//       const year = new Date(launch.window_start).getFullYear();
//       if (year >= 1957 && year <= 2025) {
//         yearCounts[year] = (yearCounts[year] || 0) + 1;
//       }
//     } catch (error) {
//       console.warn("Invalid date:", launch.window_start);
//     }
//   });

//   // Группируем по периодам (как в вашем примере)
//   const periods = [
//     { range: "1957-1985", start: 1957, end: 1985 },
//     { range: "1986-2000", start: 1986, end: 2000 },
//     { range: "2001-2015", start: 2001, end: 2015 },
//     { range: "2016-2025", start: 2016, end: 2025 }
//   ];

//   return periods.map(period => {
//     const launchesInPeriod = Object.entries(yearCounts)
//       .filter(([year]) => {
//         const yearNum = parseInt(year);
//         return yearNum >= period.start && yearNum <= period.end;
//       })
//       .reduce((sum, [, count]) => sum + count, 0);

//     return {
//       year: period.range,
//       launches: launchesInPeriod
//     };
//   }).filter(item => item.launches > 0);
// };

export default getLaunchesByYear;