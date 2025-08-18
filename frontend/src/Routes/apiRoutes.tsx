import { Routes, Route } from 'react-router-dom';
import { DashboardPage } from '../Pages/Dashboard/index';
import { LoginPage } from '../Pages/Login/index';
import { UsersPage } from '../Pages/Admin/Users';
import { ProtectedRoute } from '../Components/ProtectedRoute';
import { Layout } from '../Components/Layout/Layout';

export const AppRoutes = () => (
  <Routes>
    <Route path="/login" element={<LoginPage />} />
    
    <Route element={<ProtectedRoute>
                    <Layout />
                  </ProtectedRoute>}>
      <Route path="/" element={<DashboardPage />} />
      <Route path="/admin" element={<UsersPage />} />
    </Route>
    
    {/* <Route path="*" element={<NotFound />} /> */}
  </Routes>
);