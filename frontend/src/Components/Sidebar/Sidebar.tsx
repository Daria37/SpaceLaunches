import React from "react";
import { Link } from "react-router";
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, ResponsiveContainer, PieChart, Pie, Cell, Legend } from "recharts";

type Props = {};

// Моковые данные для примеров (замените на реальные данные из API)
const yearlyData = [
  { year: "2020", launches: 45 },
  { year: "2021", launches: 62 },
  { year: "2022", launches: 78 },
  { year: "2023", launches: 91 },
  { year: "2024", launches: 34 }
];

const countryData = [
  { country: "USA", launches: 120, color: "#3B82F6" },
  { country: "Russia", launches: 85, color: "#EF4444" },
  { country: "China", launches: 95, color: "#10B981" },
  { country: "EU", launches: 45, color: "#8B5CF6" },
  { country: "Other", launches: 35, color: "#6366F1" }
];

const Sidebar = (props: Props) => {
  return (
    <nav className="w-1/3 block py-4 px-6 top-0 bottom-0 w-64 bg-white shadow-xl left-0 absolute flex-row flex-nowrap md:z-10 z-9999 transition-all duration-300 ease-in-out transform md:translate-x-0 -translate-x-full">
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
            <span className="font-semibold">379</span>
          </div>
          <div className="flex justify-between">
            <span className="text-gray-600">Success Rate:</span>
            <span className="font-semibold text-green-600">92.3%</span>
          </div>
          <div className="flex justify-between">
            <span className="text-gray-600">This Year:</span>
            <span className="font-semibold">34</span>
          </div>
        </div>
      </div>
    </div>
    </div>
    </div>
    </nav>
  );
};

export default Sidebar;