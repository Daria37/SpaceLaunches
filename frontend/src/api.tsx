import axios from "axios"
import { ISpaceLaunch, ISpaceLaunchesResponse } from "./spacelaunches";

export const fetchLaunches = async (): Promise<ISpaceLaunch[]> => {
  try {
    const response = await axios.get(
      "https://ll.thespacedevs.com/2.2.0/launch/?format=json&limit=10"
    );
    return response.data.results; // Важно: SpaceDevs возвращает данные в поле results
  } catch (error) {
    console.error("Error fetching launches:", error);
    return []; // Возвращаем пустой массив при ошибке
  }
};

// export interface DataLaunches {
//   Data: ISpaceLaunch[];
// }

// export const Launches = async (query: string) => {
//     try {
//         const data = await axios.get<DataLaunches>("https://ll.thespacedevs.com/2.2.0/launch/?format=json&limit=10");
//         return data;
//     } catch (error) {
//         if (axios.isAxiosError(error)) {
//         console.log("error message: ", error.message);
//         return error.message;
//         } else {
//         console.log("unexpected error: ", error);
//         return "An expected error has occured.";
//         }
//     }
// }