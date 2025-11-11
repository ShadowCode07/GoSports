import React, { useState, useEffect } from "react";
import "./App.css";
import "./SideBar.css";

import Map from "./Components/Map/Map";
import LocationFilter from "./Components/LocationFilter/LocationFilter";
import { getAllLocations } from "./lib/LocationsCall";
import { LocationGet } from "./Models/Location";
import AuthForm from "./Components/AuthForm/AuthForm";

function App() {
  const [isOpen, setIsOpen] = useState(false);
  const [locations, setLocations] = useState<LocationGet[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string>("");

  useEffect(() => {
    const fetchInitial = async () => {
      try {
        setLoading(true);
        setError("");
        const data = await getAllLocations();
        setLocations(data);
      } catch (err: any) {
        console.error(err);
        setError("Failed to load locations.");
      } finally {
        setLoading(false);
      }
    };

    fetchInitial();
  }, []);

  const handleFilterResults = (data: LocationGet[]) => {
    setLocations(data);
  };

  return (
    <div className="app-shell">
      <AuthForm></AuthForm>
    </div>
  );
}

export default App;
