import React from 'react';
import { createRoot } from 'react-dom/client';
import App from './App'; // Adjust the path if the App component is located in a different folder.

createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);
