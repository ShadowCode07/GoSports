// src/lib/AuthCall.ts
import axios from "axios";

const baseURL = "https://localhost:7112/api";

// ---------- Token helpers ----------
export function getToken(): string | null {
  return (
    localStorage.getItem("auth_token") ||
    sessionStorage.getItem("auth_token")
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

export const api = axios.create({ baseURL });

if (memoryToken) {
  api.defaults.headers.common["Authorization"] = `Bearer ${memoryToken}`;
}

export type LoginRequest = {
  email: string;
  password: string;
  remember: boolean;
};

export type LoginResponse = {
  token: string;
  user?: { id: string; email: string; username: string };
};

export type RegisterRequest = {
  email: string;
  username: string;
  password: string;
};

export type RegisterResponse = {
  id?: string;
  email?: string;
  username?: string;
  token?: string;
};

export async function login({ email, password, remember }: LoginRequest) {
  const res = await api.post<LoginResponse>("/account/login", {
    email,
    password,
  });

  const token = res.data?.token;
  if (!token) {
    throw new Error("Login succeeded but no token was returned.");
  }

  setAuthToken(token, remember);
  return res.data;
}

export async function register(data: RegisterRequest) {
  const res = await api.post<RegisterResponse>("/account/register", data);
  return res.data;
}
