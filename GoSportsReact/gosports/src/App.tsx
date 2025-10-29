import React, { useState, useEffect } from "react";
import "./App.css";
import "./SideBar.css";

import Map from "./Components/Map/Map";
import LocationFilter from "./Components/LocationFilter/LocationFilter";
import { getAllLocations } from "./api";
import { LocationGet } from "./Models/Location";

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
      <button
        className="hamburger-btn"
        onClick={() => setIsOpen((prev) => !prev)}
      >
        <span className="hamb-line" />
        <span className="hamb-line" />
        <span className="hamb-line" />
      </button>

      <aside
        className={`side-panel ${isOpen ? "side-panel-open" : ""}`}
        aria-hidden={!isOpen}
      >
        <div className="side-panel-inner">
          <LocationFilter onResults={handleFilterResults} />
        </div>
      </aside>

      <main className="map-area">
        <Map locations={locations} loading={loading} error={error} />
      </main>
    </div>
  );
}

export default App;
