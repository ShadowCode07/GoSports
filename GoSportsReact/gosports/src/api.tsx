import axios from 'axios';
import type { LocationGet } from "./Models/Location";
import { LocationQuery } from './Models/Queries/LocationQuery';

const baseURL = 'https://localhost:7112/api'


export const getAllLocations = async (query?: LocationQuery) => {
    try {
        const responce = await axios.get<LocationGet[]>(`${baseURL}/Locations`,{
            params: query
        });

        console.log(responce);
        console.log(responce.data);

        return responce.data;
    } catch (error) {
        console.error('Error fetching movies: ', error);
        throw error;
    }
}