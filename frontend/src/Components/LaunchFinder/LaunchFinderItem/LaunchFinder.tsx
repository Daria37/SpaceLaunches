import React from "react";
import { Link } from "react-router-dom";
import { ISpaceLaunch } from "../../../spacelaunches";

type Props = {
  launch: ISpaceLaunch;
};

const LaunchFinderItem = ({ launch }: Props) => {
  const launchId = launch.id || Math.random().toString(36).substr(2, 9);
  
  // Используем название миссии вместо статуса
  const missionName = launch.name || `Launch ${launchId}`;
  
  // Создаем короткое название для отображения (первые 15 символов)
  const displayName = missionName.length > 15 
    ? `${missionName.substring(0, 15)}...` 
    : missionName;

  // Определяем цвет в зависимости от статуса
  const getStatusColor = () => {
    const status = launch.status.abbrev?.toLowerCase() || "";
    if (status.includes("success") || status.includes("go")) {
      return "bg-green-100 text-green-800 border-green-200 hover:bg-green-200";
    } else if (status.includes("fail") || status.includes("hold") || status.includes("tbd")) {
      return "bg-red-100 text-red-800 border-red-200 hover:bg-red-200";
    } else {
      return "bg-gray-100 text-gray-800 border-gray-200 hover:bg-gray-200";
    }
  };

  return (
    <Link
      reloadDocument
      to={`/launch/${launchId}/launch-details`}
      type="button"
      className={`inline-flex items-center px-4 py-2 text-sm font-medium border rounded-lg focus:z-10 focus:ring-2 focus:ring-blue-700 m-1 ${getStatusColor()}`}
      title={`${missionName} - ${launch.status.abbrev}`} // Показываем и название и статус в tooltip
    >
      {displayName}
    </Link>
  );
};

export default LaunchFinderItem;