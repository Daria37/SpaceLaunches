import { useEffect } from 'react';
import { Table, Button, Title, LoadingOverlay } from '@mantine/core';
import { useAppDispatch, useAppSelector } from '../../store/store';
// import { getUsers } from '../../utils/auth';
import { ProtectedRoute } from '../../Components/ProtectedRoute';
import { fetchUsers } from '../../store/slices/adminSlice'; // Используем экшен из слайса
import type { User } from '../../Models/types';

export const UsersPage = () => {
  const dispatch = useAppDispatch();
  const {
    users,
    loading,
    error
  } = useAppSelector((state) => state.admin);
  const { role } = useAppSelector((state) => state.auth);

  useEffect(() => {
    if (role === 'admin') {
      dispatch(fetchUsers());
    }
  }, [dispatch, role]);

  if (error) return <div>Error loading users: {error}</div>;

  return (
    <ProtectedRoute requiredRoles={['admin']}>
      <div style={{ position: 'relative' }}>
        <LoadingOverlay visible={loading} />
        <Title mb="md">User Management</Title>
        
        <Table striped highlightOnHover>
          <thead>
            <tr>
              <th>Email</th>
              <th>Role</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {users.map((user: User) => (
              <tr key={user.id}>
                <td>{user.email}</td>
                <td>{user.role}</td>
                <td>
                  <Button 
                    variant="light" 
                    size="sm"
                    onClick={() => console.log('Edit user', user.id)}
                  >
                    Edit
                  </Button>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
      </div>
    </ProtectedRoute>
  );
};