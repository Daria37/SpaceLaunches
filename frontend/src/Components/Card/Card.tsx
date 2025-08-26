import React, { JSX } from "react";
import { Link } from "react-router-dom";
import "./Card.css";
import { ISpaceLaunch } from "../../spacelaunches";
import LaunchImage from "../Images/LaunchImage";

interface Props {
  launch: ISpaceLaunch;
}

const Card: React.FC<Props> = ({ launch }): JSX.Element => {

  return (
    <Link
      to={`/launches/${launch.id}`}
      className="block font-bold text-center text-veryDarkViolet md:text-left no-underline"
    >
      <div className="border p-4 rounded-lg shadow hover:shadow-lg transition-shadow h-full">
        <LaunchImage 
          imageUrl={launch.image} 
          alt={`Launch ${launch.name}`}
          className="w-full h-48 object-cover mb-3 rounded"
        />
        <h2 className="text-xl font-semibold mb-2">{launch.name}</h2>
        <p className="text-gray-600 mb-1">
          <span className="font-medium">Date:</span> {new Date(launch.createdOnUTC).toLocaleDateString()}
        </p>
        <p className="text-gray-600">
          <span className="font-medium">Rocket:</span> {launch.rocketName}
        </p>
      </div>
    </Link>
  );
};

export default Card;