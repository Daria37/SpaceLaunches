import React, { useState, useEffect } from "react";
import { getAllUsersAPI, assignRoleAPI, deleteUserAPI } from "../../Context/useAdmin";
import { AppUser } from "../../Models/User";
import { useNavigate } from "react-router-dom";
import Spinner from "../Spinner/Spinner";

const AdminPanel: React.FC = () => {
  const [users, setUsers] = useState<AppUser[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [currentUser, setCurrentUser] = useState<AppUser | null>(null);
  const navigate = useNavigate();

  const isUserAdmin = (user: AppUser): boolean => {
    return user.roles ? user.roles.includes("Admin") : false;
  };

  const isCurrentUser = (user: AppUser): boolean => {
    return currentUser ? user.userName === currentUser.userName : false;
  };

  useEffect(() => {
    const token = localStorage.getItem("token");
    const userData = localStorage.getItem("user");
    
    if (!token) {
      navigate("/login");
      return;
    }

    if (userData) {
      try {
        const userdata = JSON.parse(userData);
        if (!userdata.roles || !userdata.roles.includes("Admin")) {
          navigate("/");
          return;
        }
        setCurrentUser(userdata);
      } catch (e) {
        navigate("/login");
        return;
      }
    } else {
      navigate("/login");
      return;
    }

    loadUsers();
  }, [navigate]);

  const loadUsers = async () => {
    try {
      setLoading(true);
      const response = await getAllUsersAPI();
      if (response && response.data) {
        setUsers(response.data);
        setError(null);
      }
    } catch (err: any) {
      if (err.response?.status === 401) {
        setError("Unauthorized. Please login again.");
        navigate("/login");
      } else if (err.response?.status === 403) {
        setError("Access denied. Admin role required.");
      } else {
        setError("Failed to load users");
      }
    } finally {
      setLoading(false);
    }
  };

  const handleAssignRole = async (userName: string, role: string) => {
    try {
      const response = await assignRoleAPI(userName, role);
      if (response && response.data) {
        setSuccess(`Role ${role} assigned to ${userName}`);
        loadUsers();
      }
    } catch (err: any) {
      setError("Failed to assign role: " + (err.response?.data || err.message));
    }
  };

  const handleDeleteUser = async (userName: string) => {
    if (window.confirm(`Are you sure you want to delete user ${userName}?`)) {
      try {
        await deleteUserAPI(userName);
        setSuccess(`User ${userName} deleted successfully`);
        loadUsers();
      } catch (err: any) {
        setError("Failed to delete user: " + (err.response?.data || err.message));
      }
    }
  };

  if (loading) {
    return (
      <div style={{ padding: "20px" }}>
        <Spinner />
      </div>
    );
  }

  if (error && error.includes("Access denied")) {
    return (
      <div style={{ padding: "20px" }}>
        <h1>Access Denied</h1>
        <p>{error}</p>
        <button onClick={() => navigate("/")}>Go Home</button>
      </div>
    );
  }

  return (
    <div style={{ padding: "20px" }}>
      <h1>Admin Panel</h1>
      
      {error && (
        <div style={{ color: "red", padding: "10px", marginBottom: "10px", border: "1px solid red" }}>
          {error}
          <button onClick={() => setError(null)} style={{ marginLeft: "10px" }}>×</button>
        </div>
      )}
      
      {success && (
        <div style={{ color: "green", padding: "10px", marginBottom: "10px", border: "1px solid green" }}>
          {success}
          <button onClick={() => setSuccess(null)} style={{ marginLeft: "10px" }}>×</button>
        </div>
      )}

      <button onClick={loadUsers} style={{ padding: "10px 20px", marginBottom: "20px", color: "white", backgroundColor: "#2A52BE", borderRadius: "10px" }}>
        Refresh Users
      </button>
      
      <table style={{ width: "100%", borderCollapse: "collapse" }}>
        <thead>
          <tr style={{ backgroundColor: "#f0f0f0" }}>
            <th style={{ padding: "10px", border: "1px solid #ccc" }}>Username</th>
            <th style={{ padding: "10px", border: "1px solid #ccc" }}>Email</th>
            <th style={{ padding: "10px", border: "1px solid #ccc" }}>Email Confirmed</th>
            <th style={{ padding: "10px", border: "1px solid #ccc" }}>Roles</th>
            <th style={{ padding: "10px", border: "1px solid #ccc" }}>Actions</th>
          </tr>
        </thead>
        <tbody>
          {users.map((user) => {
            const userIsAdmin = isUserAdmin(user);
            const isCurrent = isCurrentUser(user);
            
            return (
              <tr 
                key={user.id} 
                style={{ 
                  backgroundColor: isCurrent ? "#e6f7ff" : "transparent",
                  border: isCurrent ? "2px solid #1890ff" : "1px solid #ccc"
                }}
              >
                <td style={{ padding: "10px", border: "1px solid #ccc" }}>
                  {user.userName}
                  {isCurrent && (
                    <span style={{ 
                      marginLeft: "8px",
                      backgroundColor: "#1890ff",
                      color: "white",
                      padding: "2px 6px",
                      borderRadius: "4px",
                      fontSize: "12px"
                    }}>
                      You
                    </span>
                  )}
                </td>
                <td style={{ padding: "10px", border: "1px solid #ccc" }}>{user.email}</td>
                <td style={{ padding: "10px", border: "1px solid #ccc" }}>
                  {user.emailConfirmed ? "Yes" : "No"}
                </td>
                <td style={{ padding: "10px", border: "1px solid #ccc" }}>
                  {user.roles ? user.roles.join(", ") : "No roles"}
                </td>
                <td style={{ padding: "10px", border: "1px solid #ccc" }}>
                  <div style={{ display: "flex", gap: "5px" }}>
                    {!userIsAdmin && (
                      <button
                        onClick={() => handleAssignRole(user.userName, "Admin")}
                        style={{ padding: "5px 10px", backgroundColor: "#007bff", color: "white", border: "none" }}
                      >
                        Make Admin
                      </button>
                    )}
                    {!isCurrent && (
                      <button
                        onClick={() => handleDeleteUser(user.userName)}
                        style={{ padding: "5px 10px", backgroundColor: "#dc3545", color: "white", border: "none" }}
                      >
                        Delete
                      </button>
                    )}
                    {isCurrent && (
                      <span style={{ color: "#999", fontStyle: "italic" }}>
                        Cannot delete yourself
                      </span>
                    )}
                  </div>
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
};

export default AdminPanel;