import React from "react";
import { Link } from "react-router";
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer, PieChart, Pie, Cell, Legend } from "recharts";

// interface Props {
//   yearlyData?: Array<{ year: string; launches: number }>;
//   countryData?: Array<{ country: string; launches: number; color: string }>;
// }

interface Props {}

// Моковые данные для примеров (замените на реальные данные из API)
const yearlyData = [
  { year: "1957-1970", launches: 1247 },
  { year: "1971-1985", launches: 1596 },
  { year: "1986-2000", launches: 2843 },
  { year: "2001-2015", launches: 3284 },
  { year: "2016-2025", launches: 2202 }
];

const countryData = [
  { country: "USA", launches: 3215, color: "#3B82F6" },
  { country: "Russia", launches: 3892, color: "#EF4444" },
  { country: "China", launches: 1487, color: "#10B981" },
  { country: "EU", launches: 624, color: "#8B5CF6" },
  { country: "Other", launches: 954, color: "#6366F1" }
];

// const Sidebar: React.FC<Props> = ({ yearlyData = [], countryData = [] }) => {
const Sidebar = (props: Props) => {
  const hasYearlyData = yearlyData && Array.isArray(yearlyData) && yearlyData.length > 0;
  const hasCountryData = countryData && Array.isArray(countryData) && countryData.length > 0;

  if(!hasYearlyData || !hasCountryData) {
    return (
      <nav className="w-1/4 block py-4 px-6 top-0 bottom-0 w-64 bg-white shadow-xl left-0 absolute flex-row flex-nowrap md:z-10 z-9999 transition-all duration-300 ease-in-out transform md:translate-x-0 -translate-x-full">
        <div className="flex-col min-h-full px-0 flex flex-wrap items-center justify-between w-full mx-auto overflow-y-auto overflow-x-hidden">
          <div className="flex bg-white flex-col items-stretch opacity-100 relative mt-4 overflow-y-auto overflow-x-hidden h-auto z-40 items-center flex-1 rounded w-full">
            <div className="md:flex-col md:min-w-full flex flex-col list-none">
              <div className="mb-8">
                <h2 className="text-xl font-bold text-gray-800 mb-2">Launch Analytics</h2>
                <p className="text-gray-600 text-sm">Real-time launch statistics</p>
              </div>
                <p className="mb-3 mt-3 ml-4 text-m font-semibold">
                    No results!
                  </p>
              </div>
            </div>
          </div>
        </nav>
    )
  } else {
      return (
        <nav className="mt-8 w-1/3 block py-4 px-6 top-0 bottom-0 bg-white shadow-xl left-0 absolute flex-row flex-nowrap md:z-10 z-9999 transition-all duration-300 ease-in-out transform md:translate-x-0 -translate-x-full">
          <div className="flex-col min-h-full px-0 flex flex-wrap items-center justify-between w-full mx-auto overflow-y-auto overflow-x-hidden">
            <div className="flex bg-white flex-col items-stretch opacity-100 relative mt-4 overflow-y-auto overflow-x-hidden h-auto z-40 items-center flex-1 rounded w-full">
              <div className="md:flex-col md:min-w-full flex flex-col list-none">
          {/* Заголовок */}
          <div className="mb-8">
            <h2 className="text-xl font-bold text-gray-800 mb-2">Launch Analytics</h2>
            <p className="text-gray-600 text-sm">Real-time launch statistics</p>
          </div>

          {/* График по годам */}
          <div className="mb-8">
            <h3 className="text-lg font-semibold text-gray-700 mb-4">Launches by Year</h3>
            <div className="h-64">
              <ResponsiveContainer width="100%" height="100%">
                <BarChart data={yearlyData}>
                  <CartesianGrid strokeDasharray="3 3" />
                  <XAxis dataKey="year" />
                  <YAxis />
                  <Tooltip />
                  <Bar dataKey="launches" fill="#3B82F6" radius={[4, 4, 0, 0]} />
                </BarChart>
              </ResponsiveContainer>
            </div>
          </div>

          {/* График по странам */}
          <div className="mb-8">
            <h3 className="text-lg font-semibold text-gray-700 mb-4">Launches by Country</h3>
            <div className="h-64">
              <ResponsiveContainer width="100%" height="100%">
                <PieChart>
                  <Pie
                    data={countryData}
                    cx="50%"
                    cy="50%"
                    outerRadius={80}
                    dataKey="launches"
                    label={({ country, launches }) => `${country}: ${launches}`}
                  >
                    {countryData.map((entry, index) => (
                      <Cell key={`cell-${index}`} fill={entry.color} />
                    ))}
                  </Pie>
                  <Tooltip />
                </PieChart>
              </ResponsiveContainer>
            </div>
          </div>

          {/* Статистика */}
          <div className="bg-gray-50 rounded-lg p-4">
            <h3 className="text-md font-semibold text-gray-700 mb-3">Quick Stats</h3>
            <div className="space-y-2">
              <div className="flex justify-between">
                <span className="text-gray-600">Total Launches:</span>
                <span className="font-semibold">11172</span>
              </div>
              <div className="flex justify-between">
                <span className="text-gray-600">Success Rate:</span>
                <span className="font-semibold text-green-600">92.4%</span>
              </div>
              <div className="flex justify-between">
                <span className="text-gray-600">This Year:</span>
                <span className="font-semibold">187</span>
              </div>
            </div>
          </div>
        </div>
        </div>
        </div>
        </nav>
      );
  };
}

export default Sidebar;