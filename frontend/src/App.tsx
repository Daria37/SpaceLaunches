import { Outlet } from "react-router-dom";
import "./App.css";
import Hero from "./Components/Hero/Hero";
import Navbar from "./Components/Navbar/Navbar";
import LaunchesPage from "./Pages/LaunchesPage/LaunchesPage";
import React, { useState, useEffect, JSX } from "react";
import CompanyPage from "./Pages/CompanyPage/CompanyPage";
import { ToastContainer } from "react-toastify";
import { UserProvider } from "./Context/useAuth";

const App: React.FC = (): JSX.Element => {
  return <>
  <UserProvider>
    <Navbar />
    <Outlet />
    <ToastContainer />
    </UserProvider>
    </>
};

export default App;