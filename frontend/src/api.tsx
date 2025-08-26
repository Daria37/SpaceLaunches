import axios from "axios"
import { ISpaceLaunch } from "./spacelaunches";
import { handleError } from "./Helpers/ErrorHandler";

const api = "http://localhost:8080/api/";

export const searchLaunches = async (search: string): Promise<ISpaceLaunch[]> => {
  try {
    const response = await axios.get(
      `https://ll.thespacedevs.com/2.2.0//launches/?search=${search}`
    );
    return response.data.results;
  } catch (error) {
    return [];
  }
};

axios.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("token");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export const getAllLaunchesFromBackend = async (): Promise<ISpaceLaunch[]> => {
  try {
    const response = await axios.get<ISpaceLaunch[]>(`${api}launches/data`);
    return response.data;
  } catch (error: any) {
    if (error.response?.status === 401) {
      localStorage.removeItem("token");
      localStorage.removeItem("user");
      window.location.href = "/login";
    }
    handleError(error);
    throw error;
  }
};

export const getLaunchByIdFromBackend = async (id: string): Promise<ISpaceLaunch> => {
  try {
    const response = await axios.get<ISpaceLaunch>(`${api}launches/${id}`);
    return response.data;
  } catch (error: any) {
    if (error.response?.status === 401) {
      localStorage.removeItem("token");
      localStorage.removeItem("user");
      window.location.href = "/login";
    }
    handleError(error);
    throw error;
  }
};

export const searchLaunchesFromBackend = async (searchTerm: string): Promise<ISpaceLaunch[]> => {
  try {
    const response = await axios.get<ISpaceLaunch[]>(`${api}launches/search`, {
      params: { searchTerm }
    });
    return response.data;
  } catch (error: any) {
    handleError(error);
    throw error;
  }
};

export const getLaunchesAfter2020 = async (): Promise<ISpaceLaunch[]> => {
  try {
    const response = await axios.get<ISpaceLaunch[]>(`${api}launches/after-2020`);
    return response.data;
  } catch (error: any) {
    handleError(error);
    throw error;
  }
};

export const getSpaceXLaunches = async (): Promise<ISpaceLaunch[]> => {
  try {
    const response = await axios.get<ISpaceLaunch[]>(`${api}launches/spacex`);
    return response.data;
  } catch (error: any) {
    handleError(error);
    throw error;
  }
};

export const getAllUsersAPI = async () => {
  try {
    const token = localStorage.getItem("jwtToken");
    const data = await axios.get(api + "users", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return data;
  } catch (error) {
    handleError(error);
  }
};

export const assignRoleAPI = async (userName: string, role: string) => {
  try {
    const token = localStorage.getItem("jwtToken");
    const data = await axios.post(
      `${api}users/${userName}/roles`,
      `"${role}"`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      }
    );
    return data;
  } catch (error) {
    handleError(error);
  }
};

export const deleteUserAPI = async (userName: string) => {
  try {
    const token = localStorage.getItem("jwtToken");
    const data = await axios.delete(`${api}users/${userName}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return data;
  } catch (error) {
    handleError(error);
  }
};

export const fetchLaunches = async (limit: number = 10): Promise<ISpaceLaunch[]> => {
  try {
    const response = await axios.get(
      `https://ll.thespacedevs.com/2.2.0/launch/?format=json&limit=${limit}`
    );
    
    if (response.data && Array.isArray(response.data.results)) {
      return response.data.results;
    } else {
      return []
    }
    
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return [];
    }
    return [];
  }
};