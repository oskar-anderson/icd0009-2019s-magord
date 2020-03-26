import { IArea } from './../domain/IArea';
import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';

@autoinject
export class AreaService {
    constructor(private httpClient: HttpClient) {

    }

    private readonly _baseUrl = 'https://localhost:5001/api/Areas'

    getAreas(): Promise<IArea[]> {
        return this.httpClient
            .fetch(this._baseUrl, { cache: "no-store" })
            .then(response => response.json())
            .then((data: IArea[]) => data)
            .catch(reason => {
                console.error(reason);
                return [];
            });
    }

    getArea(id: string): Promise<IArea | null> {
        return this.httpClient
            .fetch(this._baseUrl + '/' + id, { cache: "no-store" })
            .then(response => response.json())
            .then((data: IArea) => data)
            .catch(reason => {
                console.error(reason);
                return null;
            });
    }

    createArea(area: IArea): Promise<string> {
        return this.httpClient.post(this._baseUrl, JSON.stringify(area), {
            cache: 'no-store'
        }).then(
            response => {
                console.log('createArea response', response);
                return response.statusText;
            }
        ).catch(reason => {
            console.error(reason);
            return JSON.stringify(reason);
        });
    }

    updateArea(area: IArea): Promise<string> {
        return this.httpClient.put(this._baseUrl + '/' + area.id, JSON.stringify(area), {
            cache: 'no-store'
        }).then(
            response => {
                console.log('updateArea response', response);
                return response.statusText;
            }
        ).catch(reason => {
            console.error(reason);
            return JSON.stringify(reason);
        });
    }

    deleteArea(area: IArea): Promise<string> {
        return this.httpClient.delete(this._baseUrl + '/' + area.id, JSON.stringify(area), {
            cache: 'no-store'
        }).then(
            response => {
                console.log('deleteArea response', response);
                return response.statusText;
            }
        ).catch(reason => {
            console.error(reason);
            return JSON.stringify(reason);
        });
    }
}
