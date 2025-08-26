import React, { JSX, SyntheticEvent } from "react";
import Card from "../Card/Card";
import { ISpaceLaunch } from "../../spacelaunches";

interface Props {
  launches: ISpaceLaunch[];
}

const CardList: React.FC<Props> = ({ launches }): JSX.Element => {
  return (
    <div className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-4 ml-72">
      {launches.length > 0 ? (
        launches.map((launch) => {
          return (
            <Card key={launch.id} launch={launch} />
          );
        })
      ) : (
        <p className="mb-3 mt-3 ml-80 text-xl font-semibold text-center md:text-xl">
          No results!
        </p>
      )}
    </div>
  );
};

export default CardList;