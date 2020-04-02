import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';


import { IDrink } from 'domain/IDrink/IDrink';
import { IDrinkCreate } from 'domain/IDrink/IDrinkCreate';
import { IDrinkEdit } from 'domain/IDrink/IDrinkEdit';




@autoinject
export class DrinkService {
    constructor(private appState: AppState, private httpClient: HttpClient) {
        this.httpClient.baseUrl = this.appState.baseUrl;
    }

    private readonly _baseUrl = 'Drinks'

    async getDrinks(): Promise<IFetchResponse<IDrink[]>> {
        try {
            const response = await this.httpClient
                .fetch(this._baseUrl, {
                    cache: "no-store",
                });
            // happy case
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IDrink[];
                console.log(data);
                return {
                    statusCode: response.status,
                    data: data
                }
            }
            // something went wrong
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }

        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }


    async getDrink(id: string): Promise<IFetchResponse<IDrink>> {
        try {
            const response = await this.httpClient
                .fetch(this._baseUrl + '/' + id, {
                    cache: "no-store",
                });

            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IDrink;
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }

        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }


    async createDrink(drink: IDrinkCreate): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient
                .post(this._baseUrl, JSON.stringify(drink), {
                    cache: 'no-store',
                })

            if (response.status >= 200 && response.status < 300) {
                console.log('response', response);
                return {
                    statusCode: response.status
                    // no data
                }
            }

            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }
        }
        catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }


    async updateDrink(drink: IDrinkEdit): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient
                .put(this._baseUrl + '/' + drink.id, JSON.stringify(drink), {
                    cache: 'no-store',
                });

            if (response.status >= 200 && response.status < 300) {
                return {
                    statusCode: response.status
                    // no data
                }
            }
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }
        }
        catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }


    async deleteDrink(id: string): Promise<IFetchResponse<string>> {

        try {
            const response = await this.httpClient
            .delete(this._baseUrl + '/' + id, null, {
                cache: 'no-store',
            });

            if (response.status >= 200 && response.status < 300) {
                return {
                    statusCode: response.status
                    // no data
                }
            }
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }
        }
        catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

}
