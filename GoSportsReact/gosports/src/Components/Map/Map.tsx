import "./Map.css"
import "leaflet/dist/leaflet.css"

import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet";
import MarkerClusterGroup from "react-leaflet-cluster";
import { Icon, divIcon, point } from "leaflet";

type Props = {}

type SimpleMarker = {
    geocode: [number, number];
    popUp: string
}

const markers: SimpleMarker[] = [
  {
    geocode: [51.41, 5.4323],
    popUp: "Hello, I am pop up 1"
  },
  {
    geocode: [51.43, 5.4625],
    popUp: "Hello, I am pop up 2"
  },
  {
    geocode: [51.44, 5.4712],
    popUp: "Hello, I am pop up 3"
  }
];

const customIcon = new Icon({
    iconUrl: "https://cdn-icons-png.flaticon.com/128/9131/9131546.png",
    iconSize: [38, 38]
})

const createClusterCustomIcon = function (cluster: any) {
  return divIcon({
    html: `<span class="cluster-icon">${cluster.getChildCount()}</span>`,
    className: "custom-marker-cluster",
    iconSize: point(33, 33, true)
  });
};

const Map = (props: Props) => {
  return (
        <MapContainer center={[51.4231, 5.4623]} zoom={13}>
        <TileLayer
          attribution='&copy; <a href="https://www.jawg.io">Jawg Maps</a> contributors'
          url="https://{s}.tile.jawg.io/jawg-streets/{z}/{x}/{y}{r}.png?access-token=vdZfXLU11SocRzVHCftt2BaXk5RyOnr7Lq22YNIC7aYQX2MtyjJUXG85deeDpxpR"
        />
        <MarkerClusterGroup
          chunkedLoading
          iconCreateFunction={createClusterCustomIcon}
        >
          {markers.map(marker => (
              <Marker position={marker.geocode} icon={customIcon}>
                  <Popup><h2>{marker.popUp}</h2></Popup>
              </Marker>    
          ))

          }
        </MarkerClusterGroup>

      </MapContainer>
  )
}

export default Map
