import React, { JSX, SyntheticEvent } from "react";
import Card from "../Card/Card";
import { v4 as uuidv4 } from "uuid";
import { ISpaceLaunch } from "../../spacelaunches";

interface Props {
  launches: ISpaceLaunch[];
}

const CardList: React.FC<Props> = ({ launches }): JSX.Element => {
  return (
    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-2 gap-4">
      {launches.length > 0 ? (
        launches.map((launch) => {
          return (
            <Card key={launch.id} launch={launch} />
          );
        })
      ) : (
        <p className="mb-3 mt-3 text-xl font-semibold text-center md:text-xl">
          No results!
        </p>
      )}
    </div>
  );
};

// const CardList: React.FC<Props> = ({
//   searchResults,
//   onPortfolioCreate,
// }: Props): JSX.Element => {
//   return (
//     <div>
//       {searchResults.length > 0 ? (
//         searchResults.map((result) => {
//           return (
//             <Card
//               id={result.symbol}
//               key={uuidv4()}
//               searchResult={result}
//               onPortfolioCreate={onPortfolioCreate}
//             />
//           );
//         })
//       ) : (
//         <p className="mb-3 mt-3 text-xl font-semibold text-center md:text-xl">
//           No results!
//         </p>
//       )}
//     </div>
//   );
// };

export default CardList;