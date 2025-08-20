import { fetchLaunches } from '../../api';
import CardList from '../../Components/CardList/CardList';
import Sidebar from '../../Components/Sidebar/Sidebar';
import { ISpaceLaunch } from "../../spacelaunches";
import React, { useState, useEffect, JSX } from "react";

const LaunchesPage = () => {
  const [launches, setLaunches] = useState<ISpaceLaunch[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const loadData = async () => {
      try {
        setLoading(true);
        const data = await fetchLaunches();
        setLaunches(data);
      } catch (err) {
        setError("Failed to load launches");
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    loadData();
  }, []);

  if (loading) return <div className="text-center py-8">Loading...</div>;
  if (error) return <div className="text-center text-red-500 py-8">{error}</div>;
  return (
    <div className="w-full relative flex ct-docs-disable-sidebar-content overflow-x-hidden">
      <Sidebar />
      <div className="container mx-auto px-64 py-8 ml-80">
          <h1 className="text-3xl font-bold mb-6 text-center">Space Launches</h1>
          <CardList launches={launches} />
        </div>
    </div>
  )
 };

 export default LaunchesPage;