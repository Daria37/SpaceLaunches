import { Navigate } from 'react-router-dom';
import { useAppSelector } from '../store/store';
import { JSX } from 'react';
import { useAuth } from '../Context/AuthContext';

interface ProtectedRouteProps {
  children: JSX.Element;
  requiredRoles?: ('admin' | 'user')[];
}

export const ProtectedRoute = ({ 
  children, 
  requiredRoles 
}: ProtectedRouteProps) => {
  const { token, role } = useAppSelector((state) => state.auth);
  const { user } = useAuth();

  if (!token) {
    return <Navigate to="/login" replace />;
  }

  if (requiredRoles && (!role || !requiredRoles.includes(role))) {
    return <Navigate to="/" replace />;
  }

  // return children;
  return user ? children : <Navigate to="/login" />;
};