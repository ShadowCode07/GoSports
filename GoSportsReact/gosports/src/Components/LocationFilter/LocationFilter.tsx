import React, { useState } from "react";
import { getAllLocations } from "../../api";
import { LocationQuery } from "../../Models/Queries/LocationQuery";
import "./LocationFilter.css";

type Props = {
  onResults?: (data: any[]) => void;
};

const LocationFilter: React.FC<Props> = ({ onResults }) => {
  const [lobbyName, setLobbyName] = useState("");
  const [locationName, setLocationName] = useState("");
  const [locationTypeName, setLocationTypeName] = useState("");
  const [sportName, setSportName] = useState("");
  const [surface, setSurface] = useState("");

  const [isIndoor, setIsIndoor] = useState<"" | "true" | "false">("");
  const [hasLights, setHasLights] = useState<"" | "true" | "false">("");

  const [sortBy, setSortBy] = useState<keyof LocationQuery | "">("");
  const [isDescending, setIsDescending] = useState(false);

  const [loading, setLoading] = useState(false);
  const [errorMsg, setErrorMsg] = useState<string | null>(null);

  const handleApplyFilters = async (e: React.FormEvent) => {
    e.preventDefault();

    const query: LocationQuery = {};

    if (lobbyName.trim()) query.LobbyName = lobbyName.trim();
    if (locationName.trim()) query.LocationName = locationName.trim();
    if (locationTypeName.trim())
      query.LocationTypeName = locationTypeName.trim();
    if (sportName.trim()) query.SportName = sportName.trim();
    if (surface.trim()) query.Surface = surface.trim();

    if (isIndoor !== "") {
      query.IsIndoor = isIndoor === "true";
    }

    if (hasLights !== "") {
      query.HasLights = hasLights === "true";
    }

    if (sortBy !== "") query.SortBy = sortBy;
    if (isDescending) query.IsDescending = true;

    try {
      setLoading(true);
      setErrorMsg(null);

      const data = await getAllLocations(query);

      if (onResults) {
        onResults(data);
      }
    } catch (err) {
      console.error(err);
      setErrorMsg("Couldn't load locations. Please try again.");
    } finally {
      setLoading(false);
    }
  };

  const handleClear = () => {
    setLobbyName("");
    setLocationName("");
    setLocationTypeName("");
    setSportName("");
    setSurface("");
    setIsIndoor("");
    setHasLights("");
    setSortBy("");
    setIsDescending(false);
    setErrorMsg(null);
  };

  return (
    <form onSubmit={handleApplyFilters} className="lf-form">
      <h2 className="lf-title">Location Filters</h2>

      <div className="lf-grid">
        <div className="lf-field">
          <label className="lf-label">Location name</label>
          <input
            type="text"
            value={locationName}
            onChange={(e) => setLocationName(e.target.value)}
            placeholder="e.g. Court A"
            className="lf-input"
          />
        </div>

        <div className="lf-field">
          <label className="lf-label">Lobby name</label>
          <input
            type="text"
            value={lobbyName}
            onChange={(e) => setLobbyName(e.target.value)}
            placeholder="e.g. Main Hall"
            className="lf-input"
          />
        </div>

        <div className="lf-field">
          <label className="lf-label">Location type</label>
          <input
            type="text"
            value={locationTypeName}
            onChange={(e) => setLocationTypeName(e.target.value)}
            placeholder="e.g. Gym / Field"
            className="lf-input"
          />
        </div>
      </div>

      <div className="lf-grid">
        <div className="lf-field">
          <label className="lf-label">Sport</label>
          <input
            type="text"
            value={sportName}
            onChange={(e) => setSportName(e.target.value)}
            placeholder="e.g. Basketball"
            className="lf-input"
          />
        </div>

        <div className="lf-field">
          <label className="lf-label">Surface</label>
          <input
            type="text"
            value={surface}
            onChange={(e) => setSurface(e.target.value)}
            placeholder="e.g. Hardwood / Grass / Turf"
            className="lf-input"
          />
        </div>

        <div className="lf-field">
          <label className="lf-label">Indoor / Outdoor</label>
          <select
            value={isIndoor}
            onChange={(e) =>
              setIsIndoor(e.target.value as "" | "true" | "false")
            }
            className="lf-select"
          >
            <option value="">Any</option>
            <option value="true">Indoor</option>
            <option value="false">Outdoor</option>
          </select>
        </div>
      </div>

      <div className="lf-grid">
        <div className="lf-field">
          <label className="lf-label">Has lights</label>
          <select
            value={hasLights}
            onChange={(e) =>
              setHasLights(e.target.value as "" | "true" | "false")
            }
            className="lf-select"
          >
            <option value="">Any</option>
            <option value="true">Yes</option>
            <option value="false">No</option>
          </select>
        </div>

        <div className="lf-field">
          <label className="lf-label">Sort by</label>
          <select
            value={sortBy}
            onChange={(e) =>
              setSortBy(e.target.value as keyof LocationQuery | "")
            }
            className="lf-select"
          >
            <option value="">Default</option>
            <option value="LobbyName">Lobby name</option>
            <option value="LocationName">Location name</option>
            <option value="LocationTypeName">Location type</option>
            <option value="SportName">Sport</option>
            <option value="Surface">Surface</option>
          </select>
        </div>

        <div className="lf-checkbox-wrapper">
          <input
            id="desc"
            type="checkbox"
            checked={isDescending}
            onChange={(e) => setIsDescending(e.target.checked)}
            className="lf-checkbox"
          />
          <label htmlFor="desc" className="lf-checkbox-label">
            Descending
          </label>
        </div>
      </div>

      <div className="lf-actions">
        <button type="submit" disabled={loading} className="lf-btn-primary">
          {loading ? "Loading..." : "Apply filters"}
        </button>

        <button
          type="button"
          onClick={handleClear}
          className="lf-btn-secondary"
        >
          Clear
        </button>

        {errorMsg && <p className="lf-error">{errorMsg}</p>}
      </div>
    </form>
  );
};

export default LocationFilter;
