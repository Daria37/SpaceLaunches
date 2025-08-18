export interface ISpaceLaunch {
  id: string;
  name: string;
//   status: {
//     name: string;
//   };
  window_start: string;
  launch_service_provider: {
    id: number;
    name: string;
    type: string;
  }
  rocket: {
    id: number;
    configuration: {
      name: string;
    };
  };
  pad: {
    location: {
      country_code: string;
    };
  };
}

export interface SpaceLaunchSearch {
  name: string;
//   status: {
//     name: string;
//   };
}

export interface ISpaceLaunchesResponse {
  count: number;
  next: string | null;
  previous: string | null;
  results: ISpaceLaunch[];
}