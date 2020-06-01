import { IDrinkCreate } from './../domain/IDrink/IDrinkCreate';
import { IFetchResponse } from './../types/IFetchResponse';
import { IDrink } from '../domain/IDrink/IDrink';
import Axios from 'axios'
import { IDrinkEdit } from '@/domain/IDrink/IDrinkEdit';

export abstract class DrinkApi {
    private static axios = Axios.create(
        {
            baseURL: "https://localhost:5001/api/v1.0/Drinks/",
            headers: {
                common: {
                    'Content-Type': 'application/json'
                }
            }
        }
    )

    static async getAllDrinks(jwt: string): Promise<IDrink[]> {
        const url = "";
        try {
            const response = await this.axios.get<IDrink[]>(url, { headers: { Authorization: 'Bearer ' + jwt } });
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

    static async getDrink(id: string, jwt: string): Promise<IDrink | null> {
        const url = "" + id;
        try {
            const response = await this.axios.get<IDrink>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            if (response.status === 200) {
                return response.data;
            }
            return null;
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return null;
        }
    }

    static async createDrink(drink: IDrinkCreate, jwt: string): Promise<IFetchResponse<void>> {
        const url = ""
        try {
            const response = await this.axios.post<IDrink>(url, drink, { headers: { Authorization: 'Bearer ' + jwt } });
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

    static async updateDrink(drink: IDrinkEdit, jwt: string): Promise<IFetchResponse<string>> {
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

    static async deleteDrink(id: string, jwt: string): Promise<void> {
        const url = "" + id;
        try {
            const response = await this.axios.delete<IDrink>(url, { headers: { Authorization: 'Bearer ' + jwt } });
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
