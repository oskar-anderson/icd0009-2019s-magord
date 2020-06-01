import { IPriceCreate } from './../domain/IPrice/IPriceCreate';
import { IFetchResponse } from './../types/IFetchResponse';
import { IPrice } from '../domain/IPrice/IPrice';
import Axios from 'axios'
import { IPriceEdit } from '@/domain/IPrice/IPriceEdit';

export abstract class PriceApi {
    private static axios = Axios.create(
        {
            baseURL: "https://localhost:5001/api/v1.0/Prices/",
            headers: {
                common: {
                    'Content-Type': 'application/json'
                }
            }
        }
    )

    static async getAllPrices(jwt: string): Promise<IPrice[]> {
        const url = "";
        try {
            const response = await this.axios.get<IPrice[]>(url, { headers: { Authorization: 'Bearer ' + jwt } });
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

    static async getPrice(id: string, jwt: string): Promise<IPrice | null> {
        const url = "" + id;
        try {
            const response = await this.axios.get<IPrice>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            if (response.status === 200) {
                return response.data;
            }
            return null;
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return null;
        }
    }

    static async createPrice(drink: IPriceCreate, jwt: string): Promise<IFetchResponse<void>> {
        const url = ""
        try {
            const response = await this.axios.post<IPrice>(url, drink, { headers: { Authorization: 'Bearer ' + jwt } });
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

    static async updatePrice(drink: IPriceEdit, jwt: string): Promise<IFetchResponse<string>> {
        const url = "" + drink.id
        try {
            const response = await this.axios.put(url, drink, { headers: { Authorization: 'Bearer ' + jwt } });
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

    static async deletePrice(id: string, jwt: string): Promise<void> {
        const url = "" + id;
        try {
            const response = await this.axios.delete<IPrice>(url, { headers: { Authorization: 'Bearer ' + jwt } });
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
