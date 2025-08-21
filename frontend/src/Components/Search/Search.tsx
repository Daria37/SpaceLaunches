//  import React, { ChangeEvent, useState, SyntheticEvent, FormEvent, JSX } from "react";

// interface Props {
//   onSearchSubmit: (e: SyntheticEvent) => void;
//   search: string | undefined;
//   handleSearchChange: (e: ChangeEvent<HTMLInputElement>) => void;
// }

// const Search: React.FC<Props> = ({
//   onSearchSubmit,
//   search,
//   handleSearchChange,
// }: Props): JSX.Element => {
//   return (
//     <section className="relative bg-gray-100">
//       <div className="max-w-4xl mx-auto p-6 space-y-6">
//         <form
//           className="form relative flex flex-col w-full p-10 space-y-4 bg-darkBlue rounded-lg md:flex-row md:space-y-0 md:space-x-3"
//           onSubmit={onSearchSubmit}
//         >
//           <input
//             className="flex-1 p-3 border-2 rounded-lg placeholder-black focus:outline-none"
//             id="search-input"
//             placeholder="Search companies"
//             value={search}
//             onChange={handleSearchChange}
//           ></input>
//         </form>
//       </div>
//     </section>
//   );
// };

// export default Search;

import React, { ChangeEvent, SyntheticEvent, useState, useEffect } from "react";
import { ISpaceLaunch } from "../../spacelaunches";
import { fetchLaunches } from "../../api";

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

  // Загружаем все запуски при монтировании
  useEffect(() => {
    const loadLaunches = async () => {
      setIsLoading(true);
      try {
        const result = await fetchLaunches(50); // Загружаем больше запусков для поиска
        if (typeof result !== "string") {
          setAllLaunches(result);
        }
      } catch (error) {
        console.error("Error loading launches:", error);
      } finally {
        setIsLoading(false);
      }
    };
    loadLaunches();
  }, []);

  // Фильтрация предложений при изменении поиска
  useEffect(() => {
    if (search.length > 1) {
      const filtered = allLaunches.filter(launch =>
        launch.name.toLowerCase().includes(search.toLowerCase()) ||
        launch.rocket.configuration.name.toLowerCase().includes(search.toLowerCase()) ||
        launch.pad.location.country_code.toLowerCase().includes(search.toLowerCase())
      );
      setSuggestions(filtered.slice(0, 5)); // Показываем только 5 предложений
      setShowSuggestions(true);
    } else {
      setSuggestions([]);
      setShowSuggestions(false);
    }
  }, [search, allLaunches]);

  const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
  };

  const handleSubmit = (e: SyntheticEvent) => {
    e.preventDefault();
    if (search.trim()) {
      const results = allLaunches.filter(launch =>
        launch.name.toLowerCase().includes(search.toLowerCase())
      );
      onSearchSubmit(results);
    }
  };

  const handleSuggestionClick = (launch: ISpaceLaunch) => {
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
        {/* <div className="text-center mb-6">
          <h2 className="text-3xl font-bold text-gray-800 mb-2">
            🚀 Find Space Launches
          </h2>
          <p className="text-gray-600">
            Search by mission name, rocket, or launch site
          </p>
        </div> */}

        <form
          className="relative bg-white rounded-lg shadow-xl p-1 border border-gray-200 w-96"
          onSubmit={handleSubmit}
        >
          <div className="flex items-center">
            <div className="flex-1 relative">
              <input
                className="w-full p-3 text-base border-0 rounded-lg focus:outline-none focus:ring-1 focus:ring-blue-500"
                placeholder="Search for missions, rockets, locations..."
                value={search}
                onChange={handleSearchChange}
                onFocus={() => search.length > 1 && setShowSuggestions(true)}
              />
              
              {/* Кнопка очистки */}
              {search && (
                <button
                  type="button"
                  onClick={handleClearSearch}
                  className="absolute right-2 top-1/2 transform -translate-y-1/2 text-gray-400 hover:text-gray-600 text-sm mr-2"
                >
                  ✕
                </button>
              )}
            </div>

            {/* <button
              type="submit"
              disabled={isLoading || !search.trim()}
              className="ml-3 px-6 py-4 bg-blue-600 text-white rounded-xl font-semibold hover:bg-blue-700 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
            >
              {isLoading ? "⌛" : "🔍 Search"}
            </button> */}
          </div>

          {/* Выпадающие предложения */}
          {showSuggestions && suggestions.length > 0 && (
            <div className="absolute top-full left-0 right-0 bg-white border border-gray-200 rounded-lg shadow-md mt-1 z-50 max-h-48 overflow-y-auto">
              {suggestions.map((launch) => (
                <div
                  key={launch.id}
                  className="p-2 hover:bg-blue-50 cursor-pointer border-b border-gray-100 last:border-b-0 text-sm"
                  onClick={() => handleSuggestionClick(launch)}
                >
                  <div className="font-semibold text-gray-800">{launch.name}</div>
                  <div className="text-xs text-gray-600">
                    {launch.rocket.configuration.name} • {launch.pad.location.country_code}
                  </div>
                  <div className="text-xs text-gray-500">
                    {new Date(launch.window_start).toLocaleDateString()}
                  </div>
                </div>
              ))}
            </div>
          )}

          {/* Сообщение если нет результатов */}
          {showSuggestions && search.length > 1 && suggestions.length === 0 && !isLoading && (
            <div className="absolute top-full left-0 right-0 bg-white border border-gray-200 rounded-lg p-2 mt-1">
              <p className="text-gray-600">No launches found for "{search}"</p>
            </div>
          )}
        </form>

        {/* Статистика поиска */}
        <div className="mt-3 mr-12 flex justify-center gap-3 text-xs text-gray-600">
          <span>✨ {allLaunches.length} total launches</span>
          <span>🌍 {new Set(allLaunches.map(l => l.pad.location.country_code)).size} countries</span>
          <span>🚀 {new Set(allLaunches.map(l => l.rocket.configuration.name)).size} rocket types</span>
        </div>
      </div>
    </section>
  );
};

export default Search;