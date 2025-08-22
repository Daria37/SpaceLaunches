import { fetchLaunches } from '../../api';
import CardList from '../../Components/CardList/CardList';
import LaunchFinder from '../../Components/LaunchFinder/LaunchFinder';
import Sidebar from '../../Components/Sidebar/Sidebar';
import Spinner from '../../Components/Spinner/Spinner';
import { ISpaceLaunch } from "../../spacelaunches";
import React, { useState, useEffect } from "react";

const LaunchesPage = () => {
  const [launches, setLaunches] = useState<ISpaceLaunch[]>([]);
  const [filteredLaunches, setFilteredLaunches] = useState<ISpaceLaunch[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [currentFilter, setCurrentFilter] = useState<"all" | "success" | "failure">("all");

  useEffect(() => {
    const loadData = async () => {
      try {
        const data = await fetchLaunches();
        setLaunches(data);
        setFilteredLaunches(data);
      } catch (err) {
        setError("Failed to load launches");
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    loadData();
  }, []);

  useEffect(() => {
    // Фильтруем запуски при изменении фильтра
    if (currentFilter === "all") {
      setFilteredLaunches(launches);
    } else if (currentFilter === "success") {
      setFilteredLaunches(launches.filter(launch => 
        launch.status.abbrev?.toLowerCase().includes("success") || 
        launch.status.abbrev?.toLowerCase().includes("go")
      ));
    } else if (currentFilter === "failure") {
      setFilteredLaunches(launches.filter(launch => 
        launch.status.abbrev?.toLowerCase().includes("fail") || 
        launch.status.abbrev?.toLowerCase().includes("hold") ||
        launch.status.abbrev?.toLowerCase().includes("tbd")
      ));
    }
  }, [currentFilter, launches]);

  const handleFilterChange = (filter: "all" | "success" | "failure") => {
    setCurrentFilter(filter);
  };

  if (loading) return <div className="text-center py-8 ml-80"><Spinner /></div>;
  if (error) return <div className="text-center text-red-500 py-8">{error}</div>;
  
  return (
    <div className="w-full relative flex ct-docs-disable-sidebar-content overflow-x-hidden">
      <Sidebar />
      <div className="container mx-auto px-64 py-8 ml-80">
        <h1 className="text-3xl font-bold mb-6 text-center">Space Launches</h1>
        <LaunchFinder onFilterChange={handleFilterChange} />
        <CardList launches={filteredLaunches} />
      </div>
    </div>
  );
};

export default LaunchesPage;