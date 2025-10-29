import React, { useState } from "react";
import { getAllLocations } from "../../api";
import { LocationQuery } from "../../Models/Queries/LocationQuery";

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
    <form
      onSubmit={handleApplyFilters}
      className="w-full max-w-4xl rounded-xl border border-gray-200 bg-white p-4 shadow-sm space-y-4"
    >
      <h2 className="text-lg font-semibold text-gray-900">Location Filters</h2>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        <div className="flex flex-col">
          <label className="text-sm font-medium text-gray-700">
            Lobby name
          </label>
          <input
            type="text"
            value={lobbyName}
            onChange={(e) => setLobbyName(e.target.value)}
            placeholder="e.g. Main Hall"
            className="mt-1 rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>

        <div className="flex flex-col">
          <label className="text-sm font-medium text-gray-700">
            Location name
          </label>
          <input
            type="text"
            value={locationName}
            onChange={(e) => setLocationName(e.target.value)}
            placeholder="e.g. Court A"
            className="mt-1 rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>

        <div className="flex flex-col">
          <label className="text-sm font-medium text-gray-700">
            Location type
          </label>
          <input
            type="text"
            value={locationTypeName}
            onChange={(e) => setLocationTypeName(e.target.value)}
            placeholder="e.g. Gym / Field"
            className="mt-1 rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        <div className="flex flex-col">
          <label className="text-sm font-medium text-gray-700">Sport</label>
          <input
            type="text"
            value={sportName}
            onChange={(e) => setSportName(e.target.value)}
            placeholder="e.g. Basketball"
            className="mt-1 rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>

        <div className="flex flex-col">
          <label className="text-sm font-medium text-gray-700">Surface</label>
          <input
            type="text"
            value={surface}
            onChange={(e) => setSurface(e.target.value)}
            placeholder="e.g. Hardwood / Grass / Turf"
            className="mt-1 rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>

        <div className="flex flex-col">
          <label className="text-sm font-medium text-gray-700">
            Indoor / Outdoor
          </label>
          <select
            value={isIndoor}
            onChange={(e) =>
              setIsIndoor(e.target.value as "" | "true" | "false")
            }
            className="mt-1 rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none focus:ring-2 focus:ring-indigo-500"
          >
            <option value="">Any</option>
            <option value="true">Indoor</option>
            <option value="false">Outdoor</option>
          </select>
        </div>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        <div className="flex flex-col">
          <label className="text-sm font-medium text-gray-700">Has lights</label>
          <select
            value={hasLights}
            onChange={(e) =>
              setHasLights(e.target.value as "" | "true" | "false")
            }
            className="mt-1 rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none focus:ring-2 focus:ring-indigo-500"
          >
            <option value="">Any</option>
            <option value="true">Yes</option>
            <option value="false">No</option>
          </select>
        </div>

        <div className="flex flex-col">
          <label className="text-sm font-medium text-gray-700">Sort by</label>
          <select
            value={sortBy}
            onChange={(e) =>
              setSortBy(e.target.value as keyof LocationQuery | "")
            }
            className="mt-1 rounded-lg border border-gray-300 px-3 py-2 text-sm outline-none focus:ring-2 focus:ring-indigo-500"
          >
            <option value="">Default</option>
            <option value="LobbyName">Lobby name</option>
            <option value="LocationName">Location name</option>
            <option value="LocationTypeName">Location type</option>
            <option value="SportName">Sport</option>
            <option value="Surface">Surface</option>
          </select>
        </div>

        <div className="flex items-center gap-2 pt-6">
          <input
            id="desc"
            type="checkbox"
            checked={isDescending}
            onChange={(e) => setIsDescending(e.target.checked)}
            className="h-4 w-4 rounded border-gray-300"
          />
          <label
            htmlFor="desc"
            className="text-sm font-medium text-gray-700 select-none"
          >
            Descending
          </label>
        </div>
      </div>

      <div className="flex flex-col md:flex-row md:items-center gap-3 pt-2">
        <button
          type="submit"
          disabled={loading}
          className="rounded-lg bg-indigo-600 px-4 py-2 text-sm font-semibold text-white shadow-sm hover:bg-indigo-700 disabled:opacity-50"
        >
          {loading ? "Loading..." : "Apply filters"}
        </button>

        <button
          type="button"
          onClick={handleClear}
          className="rounded-lg border border-gray-300 bg-white px-4 py-2 text-sm font-medium text-gray-700 hover:bg-gray-50"
        >
          Clear
        </button>

        {errorMsg && (
          <p className="text-sm text-red-600">{errorMsg}</p>
        )}
      </div>
    </form>
  );
};

export default LocationFilter;
