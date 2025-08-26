import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import HomePage from "../Pages/HomePage/HomePage";
import CompanyPage from "../Pages/CompanyPage/CompanyPage";
import LaunchesPage from "../Pages/LaunchesPage/LaunchesPage";
import LoginPage from "../Pages/LoginPage/LoginPage";
import RegisterPage from "../Pages/RegisterPage/RegisterPage";
import ProtectedRoute from "./ProtectedRoute";
import AdminPanel from "../Components/Admin/AdminPanel";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <HomePage /> },
      { path: "login", element: <LoginPage /> },
      { path: "register", element: <RegisterPage /> },
      { path: "admin", element: <AdminPanel /> },
      { path: "launches", 
        element: (
        <ProtectedRoute>
          <LaunchesPage />
        </ProtectedRoute> 
        ),
      },
      { path: "launches/:id", element: <ProtectedRoute><CompanyPage /></ProtectedRoute> }
    ],
  },
]);