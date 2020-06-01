import { ICampaignCreate } from './../domain/ICampaign/ICampaignCreate';
import { IFetchResponse } from './../types/IFetchResponse';
import { ICampaign } from '../domain/ICampaign/ICampaign';
import Axios from 'axios'
import { ICampaignEdit } from '@/domain/ICampaign/ICampaignEdit';

export abstract class CampaignApi {
    private static axios = Axios.create(
        {
            baseURL: "https://localhost:5001/api/v1.0/Campaigns/",
            headers: {
                common: {
                    'Content-Type': 'application/json'
                }
            }
        }
    )

    static async getAllCampaigns(jwt: string): Promise<ICampaign[]> {
        const url = "";
        try {
            const response = await this.axios.get<ICampaign[]>(url, { headers: { Authorization: 'Bearer ' + jwt } });
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

    static async getCampaign(id: string, jwt: string): Promise<ICampaign | null> {
        const url = "" + id;
        try {
            const response = await this.axios.get<ICampaign>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            if (response.status === 200) {
                return response.data;
            }
            return null;
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return null;
        }
    }

    static async createCampaign(drink: ICampaignCreate, jwt: string): Promise<IFetchResponse<void>> {
        const url = ""
        try {
            const response = await this.axios.post<ICampaign>(url, drink, { headers: { Authorization: 'Bearer ' + jwt } });
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

    static async updateCampaign(drink: ICampaignEdit, jwt: string): Promise<IFetchResponse<string>> {
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

    static async deleteCampaign(id: string, jwt: string): Promise<void> {
        const url = "" + id;
        try {
            const response = await this.axios.delete<ICampaign>(url, { headers: { Authorization: 'Bearer ' + jwt } });
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
