import { IPersonCreate } from './../domain/IPerson/IPersonCreate';
import { IFetchResponse } from './../types/IFetchResponse';
import { IPerson } from '../domain/IPerson/IPerson';
import Axios from 'axios'
import { IPersonEdit } from '@/domain/IPerson/IPersonEdit';

export abstract class PersonApi {
    private static axios = Axios.create(
        {
            baseURL: "https://localhost:5001/api/v1.0/Persons/",
            headers: {
                common: {
                    'Content-Type': 'application/json'
                }
            }
        }
    )

    static async getAllPersons(): Promise<IPerson[]> {
        const url = "";
        try {
            const response = await this.axios.get<IPerson[]>(url);
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

    static async getPerson(id: string): Promise<IPerson | null> {
        const url = "" + id;
        try {
            const response = await this.axios.get<IPerson>(url);
            if (response.status === 200) {
                return response.data;
            }
            return null;
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return null;
        }
    }

    static async createPerson(person: IPersonCreate, jwt: string): Promise<IFetchResponse<void>> {
        const url = ""
        try {
            const response = await this.axios.post<IPerson>(url, person, { headers: { Authorization: 'Bearer ' + jwt } });
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

    static async updatePerson(person: IPersonEdit, jwt: string): Promise<IFetchResponse<string>> {
        const url = "" + person.id
        try {
            const response = await this.axios.put(url, person, { headers: { Authorization: 'Bearer ' + jwt } });
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

    static async deletePerson(id: string, jwt: string): Promise<void> {
        const url = "" + id;
        try {
            const response = await this.axios.delete<IPerson>(url, { headers: { Authorization: 'Bearer ' + jwt } });
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
