import React, { JSX, SyntheticEvent, useEffect, useState } from "react";
// import { Link } from "react-router-dom";
import "./Card.css";
import { ISpaceLaunch } from "../../spacelaunches";
import { fetchLaunches } from "../../api";
// import AddPortfolio from "../Portfolio/AddPortfolio/AddPortfolio";

interface Props {
  launch: ISpaceLaunch;
}

const Card: React.FC<Props> = ({ launch }): JSX.Element => {
  return (
    <div className="border p-4 rounded-lg shadow hover:shadow-lg transition-shadow">
      <h2 className="text-xl font-semibold">{launch.name}</h2>
      <p className="text-gray-600">
        <span className="font-medium">Date:</span> {new Date(launch.window_start).toLocaleDateString()}
      </p>
      <p className="text-gray-600">
        <span className="font-medium">Rocket:</span> {launch.rocket.configuration.name}
      </p>
      <p className="text-gray-600">
        <span className="font-medium">Country:</span> {launch.pad.location.country_code}
      </p>
    </div>
  );
};

// const Card: React.FC<Props> = ({
//   id,
//   searchResult,
// //   onPortfolioCreate,
// }: Props): JSX.Element => {
//   return (
//     <div
//       className="flex flex-col items-center justify-between w-full p-6 bg-slate-100 rounded-lg md:flex-row"
//       key={id}
//       id={id}
//     >
//       <Link
//         to={`/company/${searchResult.symbol}/company-profile`}
//         className="font-bold text-center text-veryDarkViolet md:text-left"
//       >
//         {searchResult.name} ({searchResult.symbol})
//       </Link>
//       <p className="text-veryDarkBlue">{searchResult.currency}</p>
//       <p className="font-bold text-veryDarkBlue">
//         {searchResult.exchangeShortName} - {searchResult.stockExchange}
//       </p>
//       {/* <AddPortfolio
//         onPortfolioCreate={onPortfolioCreate}
//         symbol={searchResult.symbol}
//       /> */}
//     </div>
//   );
// };

export default Card;