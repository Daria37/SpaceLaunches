import React, { useState } from "react";
import { Link } from "react-router-dom";
import logo from "./logo.png";
import "./Navbar.css";
import Search from "../Search/Search";
import { ISpaceLaunch } from "../../spacelaunches";
import { useAuth } from "../../Context/useAuth";

interface Props {}

const Navbar = (props: Props) => {
  const { isLoggedIn, user, logout } = useAuth();
  const [searchResults, setSearchResults] = useState<ISpaceLaunch[]>([]);
  const [isSearching, setIsSearching] = useState<boolean>(false);

  const handleSearchSubmit = (results: ISpaceLaunch[]) => {
    setSearchResults(results);
    setIsSearching(true);
  };

  const handleClearSearch = () => {
    setSearchResults([]);
    setIsSearching(false);
  };

  return (
    <nav className="relative container mx-auto p-4">
      <div className="flex items-center justify-between">
        <div className="flex items-center justify-between">
          <Link to="/" className="flex items-center mb-2">
            <img src={logo} alt="Space Launches Logo" className="h-20 md:h-24 w-auto" />
          </Link>
          <div className="hidden font-bold lg:flex mr-4">
            <Link to="/" className="text-black hover:text-darkBlue">
              <Search 
                onSearchSubmit={handleSearchSubmit}
                onClearSearch={handleClearSearch}
            />
            </Link>
          </div>
          <div className="hidden font-bold lg:flex">
            <Link to="/launches" className="text-black hover:text-darkBlue">
              All Launches
            </Link>
          </div>
        </div>
        {isLoggedIn() ? (
          <div className="hidden lg:flex items-center space-x-6 text-back">
            <div className="hover:text-darkBlue">Welcome, {user?.userName}</div>
            <a
              onClick={logout}
              className="px-8 py-3 font-bold rounded text-white bg-darkBlue hover:opacity-70"
            >
              Logout
            </a>
          </div>
        ) : (
          <div className="hidden lg:flex items-center space-x-6 text-back">
            <Link to="/login" className="hover:text-darkBlue">
              Login
            </Link>
            <Link
              to="/register"
              className="px-8 py-3 font-bold rounded text-white bg-darkBlue hover:opacity-70"
            >
              Signup
            </Link>
          </div>
        )}
      </div>
    </nav>
  );
};

export default Navbar;