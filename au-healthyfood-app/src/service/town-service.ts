import { ITown } from './../domain/ITown';
import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';

@autoinject
export class TownService {
    constructor(private httpClient: HttpClient) {

    }

    private readonly _baseUrl = 'https://localhost:5001/api/Towns'

    getTowns(): Promise<ITown[]> {
        return this.httpClient
            .fetch(this._baseUrl, { cache: "no-store" })
            .then(response => response.json())
            .then((data: ITown[]) => data)
            .catch(reason => {
                console.error(reason);
                return [];
            });
    }

    getTown(id: string): Promise<ITown | null> {
        return this.httpClient
            .fetch(this._baseUrl + '/' + id, { cache: "no-store" })
            .then(response => response.json())
            .then((data: ITown) => data)
            .catch(reason => {
                console.error(reason);
                return null;
            });
    }

    createTown(town: ITown): Promise<string> {
        return this.httpClient.post(this._baseUrl, JSON.stringify(town), {
            cache: 'no-store'
        }).then(
            response => {
                console.log('createTown response', response);
                return response.statusText;
            }
        ).catch(reason => {
            console.error(reason);
            return JSON.stringify(reason);
        });
    }

    updateTown(town: ITown): Promise<string> {
        return this.httpClient.put(this._baseUrl + '/' + town.id, JSON.stringify(town), {
            cache: 'no-store'
        }).then(
            response => {
                console.log('updateTown response', response);
                return response.statusText;
            }
        ).catch(reason => {
            console.error(reason);
            return JSON.stringify(reason);
        });
    }

    deleteTown(town: ITown): Promise<string> {
        return this.httpClient.delete(this._baseUrl + '/' + town.id, JSON.stringify(town), {
            cache: 'no-store'
        }).then(
            response => {
                console.log('deletetown response', response);
                return response.statusText;
            }
        ).catch(reason => {
            console.error(reason);
            return JSON.stringify(reason);
        });
    }
}
