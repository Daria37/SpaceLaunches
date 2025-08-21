import { ISpaceLaunch } from "../../../spacelaunches";

export const getLaunchStats = (launches: ISpaceLaunch[]) => {
  const total = launches.length;
  const successful = launches.filter(l => l.status.name === "Success").length;
  const successRate = total > 0 ? (successful / total) * 100 : 0;
  const currentYear = new Date().getFullYear();
  const thisYear = launches.filter(l => 
    new Date(l.window_start).getFullYear() === currentYear
  ).length;

  return {
    total,
    successRate: successRate.toFixed(1),
    thisYear
  };
};

export default getLaunchStats;