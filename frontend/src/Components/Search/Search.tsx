import React, { ChangeEvent, SyntheticEvent, useState, useEffect } from "react";
import { ISpaceLaunch } from "../../spacelaunches";
import { searchLaunchesFromBackend } from "../../api";

interface Props {
  onSearchSubmit: (results: ISpaceLaunch[]) => void;
  onClearSearch: () => void;
}

const Search: React.FC<Props> = ({ onSearchSubmit, onClearSearch }) => {
  const [search, setSearch] = useState<string>("");
  const [suggestions, setSuggestions] = useState<ISpaceLaunch[]>([]);
  const [allLaunches, setAllLaunches] = useState<ISpaceLaunch[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [showSuggestions, setShowSuggestions] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const loadInitialData = async () => {
      try {
        const initialResults = await searchLaunchesFromBackend("");
        setAllLaunches(initialResults);
      } catch (error) {
        setError("Error loading initial data");
      }
    };
    loadInitialData();
  }, []);

  useEffect(() => {
    const searchDelay = setTimeout(async () => {
      if (search.length > 1) {
        setIsLoading(true);
        try {
          const results = await searchLaunchesFromBackend(search);
          setSuggestions(results.slice(0, 5));
          setShowSuggestions(true);
        } catch (error) {
          setError("Search error");
          setSuggestions([]);
        } finally {
          setIsLoading(false);
        }
      } else if (search.length === 0) {
        setSuggestions([]);
        setShowSuggestions(false);
      }
    }, 300);

    return () => clearTimeout(searchDelay);
  }, [search]);

  const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
  };

  const handleSubmit = async (e: SyntheticEvent) => {
    e.preventDefault();
    if (search.trim()) {
      setIsLoading(true);
      try {
        const results = await searchLaunchesFromBackend(search);
        onSearchSubmit(results);
      } catch (error) {
        setError("Search submit error");
        onSearchSubmit([]);
      } finally {
        setIsLoading(false);
      }
    }
  };

  const handleSuggestionClick = async (launch: ISpaceLaunch) => {
    setSearch(launch.name);
    setShowSuggestions(false);
    onSearchSubmit([launch]);
  };

  const handleClearSearch = () => {
    setSearch("");
    setSuggestions([]);
    setShowSuggestions(false);
    onClearSearch();
  };

  return (
    <section className="bg-gradient-to-r from-blue-10 to-indigo-50 px-4">
      <div className="max-w-2xl mx-auto mt-6">

        <form
          className="relative bg-white rounded-lg shadow-xl p-1 border border-gray-200 w-96"
          onSubmit={handleSubmit}
        >
          <div className="flex items-center">
            <div className="flex-1 relative">
              <input
                className="w-full p-3 text-base border-0 rounded-lg focus:outline-none focus:ring-1 focus:ring-blue-500"
                placeholder="Search for launches..."
                value={search}
                onChange={handleSearchChange}
                onFocus={() => search.length > 1 && setShowSuggestions(true)}
                onBlur={() => setTimeout(() => setShowSuggestions(false), 200)}
              />
              
              {search && (
                <button
                  type="button"
                  onClick={handleClearSearch}
                  className="absolute right-2 top-1/2 transform -translate-y-1/2 text-gray-400 hover:text-gray-600 text-sm mr-2"
                >
                  ✕
                </button>
              )}
              
              {isLoading && (
                <div className="absolute right-8 top-1/2 transform -translate-y-1/2">
                  <div className="animate-spin rounded-full h-4 w-4 border-b-2 border-blue-500"></div>
                </div>
              )}
            </div>
            <button
              type="submit"
              className="ml-2 px-4 py-2 bg-blue-500 text-white rounded-lg hover:bg-blue-600"
            >
              Search
            </button>
          </div>

          {showSuggestions && suggestions.length > 0 && (
            <div className="absolute top-full left-0 right-0 bg-white border border-gray-200 rounded-lg shadow-md mt-1 z-50 max-h-48 overflow-y-auto">
              {suggestions.map((launch) => (
                <div
                  key={launch.id}
                  className="p-2 hover:bg-blue-50 cursor-pointer border-b border-gray-100 last:border-b-0 text-sm"
                  onMouseDown={(e) => e.preventDefault()}
                  onClick={() => handleSuggestionClick(launch)}
                >
                  <div className="font-semibold text-gray-800">{launch.name}</div>
                  <div className="text-xs text-gray-600">
                    {launch.rocketName} • {launch.countryCode}
                  </div>
                  <div className="text-xs text-gray-500">
                    {new Date(launch.createdOnUTC).toLocaleDateString()}
                  </div>
                </div>
              ))}
            </div>
          )}

          {showSuggestions && search.length > 1 && suggestions.length === 0 && !isLoading && (
            <div className="absolute top-full left-0 right-0 bg-white border border-gray-200 rounded-lg p-2 mt-1">
              <p className="text-gray-600">No launches found for "{search}"</p>
            </div>
          )}
        </form>

        <div className="mt-3 mr-12 flex justify-center gap-3 text-xs text-gray-600">
          <span>✨ {allLaunches.length} total launches</span>
          <span>🌍 {new Set(allLaunches.map(l => l.countryCode)).size} countries</span>
          <span>🚀 {new Set(allLaunches.map(l => l.rocketName)).size} rocket types</span>
        </div>
      </div>
    </section>
  );
};

export default Search;