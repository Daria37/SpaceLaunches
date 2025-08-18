// import { Outlet } from "react-router";
// import Navbar from "./Components/Navbar/Navbar";
// import "react-toastify/dist/ReactToastify.css";
import "./App.css";
import CardList from "./Components/CardList/CardList";
import Hero from "./Components/Hero/Hero";
import Navbar from "./Components/Navbar/Navbar";
// import { ToastContainer } from "react-toastify";
// import { UserProvider } from "./Context/useAuth";

import { fetchLaunches } from './api';
import { ISpaceLaunch } from "./spacelaunches";
import React, { useState, useEffect, JSX } from "react";

const App: React.FC = (): JSX.Element => {
  const [launches, setLaunches] = useState<ISpaceLaunch[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const loadData = async () => {
      try {
        setLoading(true);
        const data = await fetchLaunches();
        setLaunches(data);
      } catch (err) {
        setError("Failed to load launches");
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    loadData();
  }, []);

  if (loading) return <div className="text-center py-8">Loading...</div>;
  if (error) return <div className="text-center text-red-500 py-8">{error}</div>;

  return (
    <div className="App">
    <Navbar />
    <Hero />
    <div className="container mx-auto px-4 py-8">
      <h1 className="text-3xl font-bold mb-6 text-center">Space Launches</h1>
      <CardList launches={launches} />
    </div>
    </div>
  );
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