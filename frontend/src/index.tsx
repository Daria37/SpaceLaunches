import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter } from 'react-router-dom';
import { RouterProvider } from 'react-router';
import { router } from './Routes/Routes';
import { fetchLaunches } from './api';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

// fetchLaunches()
//   .then(data => console.log("Launches data:", data))
//   .catch(error => console.error("Error:", error));

root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
    {/* <BrowserRouter>
    <App />
    </BrowserRouter> */}
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
