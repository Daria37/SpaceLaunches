import axios from "axios";
import { handleError } from "../Helpers/ErrorHandler";
import { AppUser } from "../Models/User";

const api = "http://localhost:8080/api/";

export const getAllUsersAPI = async () => {
  try {
    const token = localStorage.getItem("token");
    const data = await axios.get<AppUser[]>(api + "users", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return data;
  } catch (error) {
    handleError(error);
    throw error;
  }
};

export const assignRoleAPI = async (userName: string, role: string) => {
  try {
    const token = localStorage.getItem("token");
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
    throw error;
  }
};

export const deleteUserAPI = async (userName: string) => {
  try {
    const token = localStorage.getItem("token");
    const data = await axios.delete(`${api}users/${userName}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    return data;
  } catch (error) {
    handleError(error);
    throw error;
  }
};