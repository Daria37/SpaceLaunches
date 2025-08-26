export interface ISpaceLaunch {
  id: string;
  name: string;
  image: string;
  status: string;
  createdOnUTC: string;
  mapImage: string;
  mission: string;
  rocketName: string;
  countryCode: string;
}

export interface CompanyProfile {
  launch_service_provider: {
    id: number;
    name: string;
    type: string;
  };
  pad: {
    location: {
      country_code: string;
      map_image: string;
    };
  };
}

export interface SearchLaunches {
  id: string;
  window_start: string;
  launch_service_provider: {
    name: string;
    type: string;
  };
  pad: {
    location: {
      country_code: string;
      map_image: string;
    };
  };
}

export interface ISpaceLaunchesResponse {
  count: number;
  next: string | null;
  previous: string | null;
  results: ISpaceLaunch[];
}