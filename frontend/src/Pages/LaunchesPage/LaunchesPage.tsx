import { useParams } from 'react-router-dom';
import CardList from '../../Components/CardList/CardList';
import LaunchFinder from '../../Components/LaunchFinder/LaunchFinder';
import Sidebar from '../../Components/Sidebar/Sidebar';
import Spinner from '../../Components/Spinner/Spinner';
import { ISpaceLaunch } from "../../spacelaunches";
import React, { useState, useEffect } from "react";
import { getAllLaunchesFromBackend, getSpaceXLaunches } from '../../api';

const LaunchesPage = () => {
  const [launches, setLaunches] = useState<ISpaceLaunch[]>([]);
  const [filteredLaunches, setFilteredLaunches] = useState<ISpaceLaunch[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [currentFilter, setCurrentFilter] = useState<"all" | "success" | "failure">("all");

  const isAdmin = (): boolean => {
    const userData = localStorage.getItem("user");
    if (userData) {
      try {
        const user = JSON.parse(userData);
        return user.roles && user.roles.includes("Admin");
      } catch (e) {
        return false;
      }
    }
    return false;
  };

  useEffect(() => {
    const loadData = async () => {
      try {
        let data: ISpaceLaunch[];
        
        if (isAdmin()) {
          data = await getAllLaunchesFromBackend();
        } else {
          data = await getSpaceXLaunches();
        }
        
        setLaunches(data);
        setFilteredLaunches(data);
      } catch (err: any) {
        setError("Failed to load launches: " + (err.message || "Unknown error"));
      } finally {
        setLoading(false);
      }
    };

    loadData();
  }, []);

  useEffect(() => {
    if (currentFilter === "all") {
      setFilteredLaunches(launches);
    } else if (currentFilter === "success") {
      setFilteredLaunches(launches.filter(launch => 
        launch.status === "Success"
      ));
    } else if (currentFilter === "failure") {
      setFilteredLaunches(launches.filter(launch => 
        launch.status === "Failure"
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
      <div className="container mx-auto px-56 py-16">
        <div className="container ml-32">
          <h1 className="text-3xl font-bold mb-6 text-center ml-4">Space Launches</h1>
          <LaunchFinder onFilterChange={handleFilterChange} />
        </div>
        <CardList launches={filteredLaunches} />
      </div>
    </div>
  );
};

export default LaunchesPage;