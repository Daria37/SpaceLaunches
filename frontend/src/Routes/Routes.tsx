import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import HomePage from "../Pages/HomePage/HomePage";
import CompanyPage from "../Pages/CompanyPage/CompanyPage";
import LaunchesPage from "../Pages/LaunchesPage/LaunchesPage";
import LoginPage from "../Pages/LoginPage/LoginPage";
import RegisterPage from "../Pages/RegisterPage/RegisterPage";
import ProtectedRoute from "./ProtectedRoute";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <HomePage /> },
      { path: "login", element: <LoginPage /> },
      { path: "register", element: <RegisterPage /> },
      { path: "launches", 
        element: (
        <ProtectedRoute>
          <LaunchesPage />
        </ProtectedRoute> 
        ),
      },
      { path: "company/:ticker", element: <ProtectedRoute><CompanyPage /></ProtectedRoute> }
    ],
  },
]);