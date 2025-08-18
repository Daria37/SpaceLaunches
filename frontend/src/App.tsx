import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { MantineProvider } from '@mantine/core';
import { LoginPage } from './Pages/Login';
import { UsersPage } from './Pages/Admin/Users';
import { DashboardPage } from './Pages/Dashboard';
import { ProtectedRoute } from './Components/ProtectedRoute';
import { Layout } from './Components/Layout/Layout';
import { AuthProvider } from './Context/AuthContext';

function App() {
  return (
    <MantineProvider>
      <AuthProvider>
      <BrowserRouter>
        <Routes>
          <Route path="/login" element={<LoginPage />} />
          <Route
            path="/"
            element={
              <ProtectedRoute>
                <Layout />
              </ProtectedRoute>
            }
          >
            <Route index element={<DashboardPage />} />
            <Route path="admin" element={<UsersPage />} />
          </Route>
        </Routes>
      </BrowserRouter>
      </AuthProvider>
    </MantineProvider>
  );
}

export default App;