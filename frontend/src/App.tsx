// import { Outlet } from "react-router";
// import Navbar from "./Components/Navbar/Navbar";
// import "react-toastify/dist/ReactToastify.css";
import { Outlet } from "react-router-dom";
import "./App.css";
import Hero from "./Components/Hero/Hero";
import Navbar from "./Components/Navbar/Navbar";
import LaunchesPage from "./Pages/LaunchesPage/LaunchesPage";
// import { ToastContainer } from "react-toastify";
// import { UserProvider } from "./Context/useAuth";

import React, { useState, useEffect, JSX } from "react";
import CompanyPage from "./Pages/CompanyPage/CompanyPage";

const App: React.FC = (): JSX.Element => {
  return <>
    <Navbar />
    <Outlet />
    </>
};

// function App() {
//   return (
//     <>
//     <CardList launches={[]} />
//       {/* <UserProvider>
//         <Navbar />
//         <Outlet />
//         <ToastContainer />
//       </UserProvider> */}
//     </>
//   );
// }

export default App;