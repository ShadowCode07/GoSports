import { SportGet } from "./Sport";

export type LobbyGet = {
  id: string;
  name: string;
  locationId: string;
  sport: SportGet;
};
