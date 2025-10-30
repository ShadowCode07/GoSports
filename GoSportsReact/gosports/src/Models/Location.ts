import { LobbyGet } from "./Lobby";
import { SportGet } from "./Sport";

export type LocationTypeGet = {
  id: string;
  locationId: string;
  name: string;
  isIndoor: boolean;
  surface: string;
  hasLights: boolean;
};

export type LocationGet = {
  id: string;
  LocationName: string;
  description: string;
  locationType: LocationTypeGet;
  latitude: number;
  longitude: number;
  lobbies: LobbyGet[];
  sports: SportGet[];
  currentLobbyCount: number;
  maxLobbyCount: number;
};