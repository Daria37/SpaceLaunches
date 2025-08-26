// src/Components/ProtectedAdminRoute.tsx
import React from "react";
import { Navigate } from "react-router-dom";

interface ProtectedAdminRouteProps {
  children: React.ReactNode;
}

const ProtectedAdminRoute: React.FC<ProtectedAdminRouteProps> = ({ children }) => {
  const isLoggedIn = !!localStorage.getItem("token");
  const userData = localStorage.getItem("user");
  
  if (!isLoggedIn) {
    return <Navigate to="/login" replace />;
  }

  if (userData) {
    const user = JSON.parse(userData);
    if (!user.roles?.includes("Admin")) {
      return <Navigate to="/" replace />;
    }
  }

  return <>{children}</>;
};

export default ProtectedAdminRoute;