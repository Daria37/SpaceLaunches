import React, { useState } from "react";

type Props = {
  onFilterChange: (filter: "all" | "success" | "failure") => void;
};

const LaunchFinder = ({ onFilterChange }: Props) => {
  const [filter, setFilter] = useState<"all" | "success" | "failure">("all");

  const handleFilterClick = (newFilter: "all" | "success" | "failure") => {
    setFilter(newFilter);
    onFilterChange(newFilter);
  };

  const getButtonClass = (buttonFilter: string) => {
    return `px-4 py-2 text-sm font-medium rounded-md border ${
      filter === buttonFilter
        ? "bg-blue-600 text-white border-blue-600"
        : "bg-white text-gray-700 border-gray-200 hover:bg-gray-100"
    }`;
  };

  return (
    <div className="m-4">
      <div className="inline-flex rounded-md shadow-sm gap-2 ml-80" role="group">
        <button
          type="button"
          className={getButtonClass("all")}
          onClick={() => handleFilterClick("all")}
        >
          All Launches
        </button>
        <button
          type="button"
          className={getButtonClass("success")}
          onClick={() => handleFilterClick("success")}
        >
          Successful
        </button>
        <button
          type="button"
          className={getButtonClass("failure")}
          onClick={() => handleFilterClick("failure")}
        >
          Failed
        </button>
      </div>
    </div>
  );
};

export default LaunchFinder;