import "./Map.css";
import "leaflet/dist/leaflet.css";

import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet";
import MarkerClusterGroup from "react-leaflet-cluster";
import { Icon, divIcon, point } from "leaflet";
import { useEffect, useState } from "react";
import { JSX } from "react/jsx-runtime";
import { LocationGet } from "../../Models/Location";
import { getAllLocations } from "../../api";

type Props = {};

const customIcon = new Icon({
  iconUrl: "https://cdn-icons-png.flaticon.com/128/9131/9131546.png",
  iconSize: [38, 38],
});

const createClusterCustomIcon = function (cluster: any) {
  return divIcon({
    html: `<span class="cluster-icon">${cluster.getChildCount()}</span>`,
    className: "custom-marker-cluster",
    iconSize: point(33, 33, true),
  });
};

const Map : React.FC<Props> = (props: Props): JSX.Element => {
  const [locations, setLocations] = useState<LocationGet[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string>("");

    useEffect(() => {
    const fetchLocations = async () => {
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

    fetchLocations();
  }, []);

  const defaultCenter: [number, number] = [51.4231, 5.4623];
  const defaultZoom = 13;

  return (
    <MapContainer center={defaultCenter} zoom={defaultZoom}>
      <TileLayer
        attribution='&copy; <a href="https://www.jawg.io">Jawg Maps</a> contributors'
        url="https://{s}.tile.jawg.io/jawg-streets/{z}/{x}/{y}{r}.png?access-token=vdZfXLU11SocRzVHCftt2BaXk5RyOnr7Lq22YNIC7aYQX2MtyjJUXG85deeDpxpR"
      />
      <MarkerClusterGroup
        maxClusterRadius={15}
        chunkedLoading
        iconCreateFunction={createClusterCustomIcon}
      >
        {loading && (
          <Popup position={defaultCenter}>
            <div>Loading locations…</div>
          </Popup>
        )}

        {error && !loading && (
          <Popup position={defaultCenter}>
            <div style={{ color: "red" }}>{error}</div>
          </Popup>
        )}

        {!loading &&
          !error &&
          locations.map((loc) => (
            <Marker
              key={loc.id}
              position={[loc.latitude, loc.longitude]}
              icon={customIcon}
            >
              <Popup>
                <h2 style={{ margin: 0 }}>{loc.name}</h2>
                <p style={{ margin: "4px 0 8px", fontSize: "0.9rem" }}>
                  {loc.description}
                </p>

                <div style={{ fontSize: "0.8rem", lineHeight: 1.4 }}>
                  <div>
                    <strong>Type:</strong> {loc.locationType?.name}{" "}
                    {loc.locationType?.isIndoor ? "(indoor)" : "(outdoor)"}
                  </div>

                  <div>
                    <strong>Surface:</strong> {loc.locationType?.surface}
                  </div>

                  <div>
                    <strong>Lights:</strong>{" "}
                    {loc.locationType?.hasLights ? "Yes" : "No"}
                  </div>

                  <div style={{ marginTop: "6px" }}>
                    <strong>Sports here:</strong>{" "}
                    {loc.sports.map((s) => s.name).join(", ")}
                  </div>

                  <div>
                    <strong>Active lobbies:</strong> {loc.currentLobbyCount}/
                    {loc.maxLobbyCount}
                  </div>

                  <div style={{ marginTop: "6px" }}>
                    <strong>Lobbies:</strong>
                    <ul style={{ margin: "4px 0", paddingLeft: "16px" }}>
                      {loc.lobbies.map((lobby) => (
                        <li key={lobby.id}>
                          {lobby.name}{" "}
                          <span style={{ opacity: 0.7 }}>
                            ({lobby.sport?.name})
                          </span>
                        </li>
                      ))}
                    </ul>
                  </div>

                  <div style={{ marginTop: "6px" }}>
                    <strong>Coords:</strong> {loc.latitude}, {loc.longitude}
                  </div>
                </div>
              </Popup>
            </Marker>
          ))}
      </MarkerClusterGroup>
    </MapContainer>
  );
};

export default Map;
