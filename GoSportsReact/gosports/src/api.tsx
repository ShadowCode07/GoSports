import axios from 'axios';
import type { LocationGet } from "./Models/Location";

const baseURL = 'https://localhost:7112/api'


export const getAllLocations = async (/*query: string*/) => {
    try {
        const responce = await axios.get<LocationGet[]>(`${baseURL}/Locations`);
        return responce.data;
    } catch (error) {
        console.error('Error fetching movies: ', error);
        throw error;
    }
}