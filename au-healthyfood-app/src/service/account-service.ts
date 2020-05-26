import { autoinject } from 'aurelia-framework';
import { AppState } from 'state/app-state';
import { HttpClient } from 'aurelia-fetch-client';
import { IFetchResponse } from 'types/IFetchResponse';
import { ILoginResponse } from 'domain/ILoginResponse';

@autoinject
export class AccountService {
    public userEmail: string = "";
    public userPassword: string = "";

    constructor(
        private appState: AppState,
        private httpClient: HttpClient) {
        this.httpClient.baseUrl = this.appState.baseUrl;
    }


    async login(email: string, password: string): Promise<IFetchResponse<ILoginResponse>> {
        try {
            const response = await this.httpClient.post('account/login', JSON.stringify({
                email: email,
                password: password,
            }), {
                cache: 'no-store'
            });

            // Everything went well!
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as ILoginResponse;
                return {
                    statusCode: response.status,
                    data: data
                }
            }


            // Something went wrong!!
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


    async register(email: string, password: string, firstName: string, lastName: string, phoneNumber: string): Promise<IFetchResponse<ILoginResponse>> {
        try {
            const response = await this.httpClient.post('account/register', JSON.stringify({
                email: email,
                password: password,
                firstName: firstName,
                lastName: lastName,
                phoneNumber: phoneNumber
            }), {
                cache: 'no-store'
            });

            // Everything went well!
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as ILoginResponse;
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            // Something went wrong!!
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

    async changeNames(email: string, firstName: string, lastName: string): Promise<IFetchResponse<ILoginResponse>> {
        try {
            const response = await this.httpClient.post('account/changeNames', JSON.stringify({
                email: email,
                firstName: firstName,
                lastName: lastName,
            }), {
                cache: 'no-store'
            });

            // Everything went well!
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as ILoginResponse;
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            // Something went wrong!!
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


    async changePassword(email: string, oldPassword: string, newPassword: string): Promise<IFetchResponse<ILoginResponse>> {
        try {
            const response = await this.httpClient.post('account/changePassword', JSON.stringify({
                email: email,
                oldPassword: oldPassword,
                newPassword: newPassword,
            }), {
                cache: 'no-store'
            });

            // Everything went well!
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as ILoginResponse;
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            // Something went wrong!!
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


    async changeEmail(email: string, newEmail: string): Promise<IFetchResponse<ILoginResponse>> {
        try {
            const response = await this.httpClient.post('account/changeEmail', JSON.stringify({
                email: email,
                newEmail: newEmail,
            }), {
                cache: 'no-store'
            });

            // Everything went well!
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as ILoginResponse;
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            // Something went wrong!!
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

    async changePhoneNumber(email: string, phoneNumber: string, newPhoneNumber: string): Promise<IFetchResponse<ILoginResponse>> {
        try {
            const response = await this.httpClient.post('account/changePhoneNumber', JSON.stringify({
                email: email,
                phoneNumber: phoneNumber,
                newPhoneNumber: newPhoneNumber,
            }), {
                cache: 'no-store'
            });

            // Everything went well!
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as ILoginResponse;
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            // Something went wrong!!
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
