import React, { useEffect, useState } from "react";
import { data, Link, useParams } from "react-router-dom";
import { CompanyProfile } from "../../spacelaunches";
import { fetchLaunches } from "../../api";
import { ISpaceLaunch } from "../../spacelaunches";
import LaunchImage from "../../Components/Images/LaunchImage";

const CompanyPage = () => {
  let { ticker } = useParams();
  const [launch, setLaunch] = useState<ISpaceLaunch | null>(null);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
  const getLaunchData = async () => {
    try {
      setLoading(true);
      const result = await fetchLaunches();
    //   console.log("Ticker from URL:", ticker);
      
      if (typeof result !== "string") {
        const foundLaunch = result.find(l => {
        //   console.log("Launch ID:", l.id, "Ticker:", ticker);
          return l.name === ticker;
        });
        setLaunch(foundLaunch || null);
      }
    } catch (err) {
      console.error("Error loading launch:", err);
    } finally {
      setLoading(false);
    }
  };
  
  getLaunchData();
}, [ticker]);

  if (loading) return <div>Loading...</div>;
  if (!launch) return <div>Launch not found</div>;

  return (
    <div>
      <h1 className="text-2xl font-bold mb-4">{launch.name}</h1>
      <div className="bg-white p-6 rounded-lg shadow">
        <LaunchImage 
          imageUrl={launch.pad.location.map_image} 
          alt={`Launch location: ${launch.pad.location.country_code}`}
          className="w-full h-full"
        />
        <p><strong>Date:</strong> {new Date(launch.window_start).toLocaleDateString()}</p>
        <p><strong>Rocket:</strong> {launch.rocket.configuration.name}</p>
        <p><strong>Type:</strong> {launch.launch_service_provider.type}</p>
        <p><strong>Location:</strong> {launch.pad.location.country_code}</p>
        <p><strong>Mission:</strong> {launch.mission.description}</p>
      </div>
    </div>
  );
};

export default CompanyPage;