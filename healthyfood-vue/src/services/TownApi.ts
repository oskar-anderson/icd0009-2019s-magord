import { ITownCreate } from './../domain/ITown/ITownCreate';
import { IFetchResponse } from './../types/IFetchResponse';
import { ITown } from '../domain/ITown/ITown';
import Axios from 'axios'
import { ITownEdit } from '@/domain/ITown/ITownEdit';

export abstract class TownApi {
    private static axios = Axios.create(
        {
            baseURL: "https://localhost:5001/api/v1.0/Towns/",
            headers: {
                common: {
                    'Content-Type': 'application/json'
                }
            }
        }
    )

    static async getAllTowns(): Promise<ITown[]> {
        const url = "";
        try {
            const response = await this.axios.get<ITown[]>(url);
            console.log('getAll response', response);
            if (response.status === 200) {
                return response.data;
            }
            return [];
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return [];
        }
    }

    static async getTown(id: string): Promise<ITown | null> {
        const url = "" + id;
        try {
            const response = await this.axios.get<ITown>(url);
            if (response.status === 200) {
                return response.data;
            }
            return null;
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return null;
        }
    }

    static async createTown(town: ITownCreate, jwt: string): Promise<IFetchResponse<void>> {
        const url = ""
        try {
            const response = await this.axios.post<ITown>(url, town, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('post response', response);
            if (response.status === 200) {
                return {
                    statusCode: response.status
                }
            }
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }
        } catch (error) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(error)
            }
        }
    }

    static async updateTown(town: ITownEdit, jwt: string): Promise<IFetchResponse<string>> {
        const url = "" + town.id
        try {
            const response = await this.axios.put(url, town, { headers: { Authorization: 'Bearer ' + jwt } });
            if (response.status === 200) {
                return {
                    statusCode: response.status
                }
            }
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }
        } catch (error) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(error)
            }
        }
    }

    static async deleteTown(id: string, jwt: string): Promise<void> {
        const url = "" + id;
        try {
            const response = await this.axios.delete<ITown>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('delete response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }
}
