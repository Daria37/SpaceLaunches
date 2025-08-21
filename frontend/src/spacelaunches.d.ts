export interface ISpaceLaunch {
  id: string;
  name: string;
  image: string;
  status: {
    name: string;
  };
  window_start: string;
  launch_service_provider: {
    id: number;
    name: string;
    type: string;
  };
  rocket: {
    id: number;
    configuration: {
      name: string;
    };
  };
  mission: {
    description: string;
  }
  pad: {
    location: {
      country_code: string;
      map_image: string;
    };
  };
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