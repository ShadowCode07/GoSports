import axios from "axios";

const baseURL = "https://localhost:7112/api";

export function getToken(): string | null {
  return (
    localStorage.getItem("auth_token") || sessionStorage.getItem("auth_token")
  );
}

let memoryToken: string | null = getToken();

export function setAuthToken(token: string, remember: boolean) {
  memoryToken = token;
  if (remember) {
    localStorage.setItem("auth_token", token);
    sessionStorage.removeItem("auth_token");
  } else {
    sessionStorage.setItem("auth_token", token);
    localStorage.removeItem("auth_token");
  }

  api.defaults.headers.common["Authorization"] = `Bearer ${token}`;
}

export function clearAuthToken() {
  memoryToken = null;
  localStorage.removeItem("auth_token");
  sessionStorage.removeItem("auth_token");
  delete api.defaults.headers.common["Authorization"];
}

export const api = axios.create({
  baseURL,
});

if (memoryToken) {
  api.defaults.headers.common["Authorization"] = `Bearer ${memoryToken}`;
}