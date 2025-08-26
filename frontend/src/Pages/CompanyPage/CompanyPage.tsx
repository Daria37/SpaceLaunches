import React, { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";
import { getLaunchByIdFromBackend } from "../../api";
import { ISpaceLaunch } from "../../spacelaunches";
import LaunchImage from "../../Components/Images/LaunchImage";
import Spinner from "../../Components/Spinner/Spinner";

const CompanyPage = () => {
  let { id } = useParams();
  const [launch, setLaunch] = useState<ISpaceLaunch | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const getLaunchData = async () => {
      if (!id) {
        setError("No launch ID provided");
        setLoading(false);
        return;
      }

      try {
        setLoading(true);
        const launchData = await getLaunchByIdFromBackend(id);
        setLaunch(launchData);
      } catch (err) {
        setError("Failed to load launch details");
      } finally {
        setLoading(false);
      }
    };
    
    getLaunchData();
  }, [id]);

  if (loading) return <Spinner />;
  if (error) return <div className="text-red-500">{error}</div>;
  if (!launch) return <div>Launch not found for ID: {id}</div>;

  return (
    <div className="container mx-auto p-4">
      <Link to="/launches" className="text-blue-500 hover:underline mb-4 inline-block">
        ← Back to Launches
      </Link>

      <div className="bg-white p-6 rounded-lg shadow">
        <h1 className="text-2xl font-bold mb-4">{launch.name}</h1>
        
        <LaunchImage 
          imageUrl={launch.mapImage} 
          alt={`Launch ${launch.name}`}
          className="w-full h-64 object-cover rounded mb-4"
        />
        
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <div>
            <h3 className="font-semibold text-lg mb-2">Launch Details</h3>
            <p><strong>Date:</strong> {new Date(launch.createdOnUTC).toLocaleDateString()}</p>
            <p><strong>Rocket:</strong> {launch.rocketName}</p>
            <p><strong>Status:</strong> {launch.status}</p>
          </div>
          
          <div>
            <h3 className="font-semibold text-lg mb-2">Location</h3>
            <p><strong>Country:</strong> {launch.countryCode}</p>
            <p><strong>Mission:</strong> {launch.mission}</p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CompanyPage;