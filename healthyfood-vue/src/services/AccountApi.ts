import { IChangePhoneNumberDTO } from './../types/IChangePhoneNumberDTO';
import { IChangePasswordDTO } from './../types/IChangePasswordDTO';
import { IChangeEmailDTO } from './../types/IChangeEmailDTO';
import { IRegisterDTO } from './../types/IRegisterDTO';
import { ILoginDTO } from './../types/ILoginDTO';
import Axios from 'axios'

interface ILoginResponse {
    token: string;
    status: string;
}

export abstract class AccountApi {
    private static axios = Axios.create(
        {
            baseURL: "https://localhost:5001/api/v1.0/",
            headers: {
                common: {
                    'Content-Type': 'application/json'
                }
            }
        }
    )

    static async register(registerDTO: IRegisterDTO): Promise<string | null> {
        const url = "account/register";
        try {
            const response = await this.axios.post<ILoginResponse>(url, registerDTO);
            if (response.status === 200) {
                localStorage.setItem('jwt', response.data.token)
                return response.data.token;
            }
            return null;
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return null;
        }
    }

    static async changeEmail(changeEmailDTO: IChangeEmailDTO): Promise<string | null> {
        const url = "account/changeEmail";
        try {
            const response = await this.axios.post<ILoginResponse>(url, changeEmailDTO);
            if (response.status === 200) {
                return response.data.token;
            }
            return null;
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return null;
        }
    }

    static async changePassword(changePasswordDTO: IChangePasswordDTO): Promise<string | null> {
        const url = "account/changePassword";
        try {
            const response = await this.axios.post<ILoginResponse>(url, changePasswordDTO);
            if (response.status === 200) {
                return response.data.token;
            }
            return null;
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return null;
        }
    }

    static async changePhoneNumber(changePhoneNumberDTO: IChangePhoneNumberDTO): Promise<string | null> {
        const url = "account/changePhoneNumber";
        try {
            const response = await this.axios.post(url, changePhoneNumberDTO);
            if (response.status === 200) {
                return response.data.token;
            }
            return null;
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return null;
        }
    }

    static async getJwt(loginDTO: ILoginDTO): Promise<string | null> {
        const url = "account/login";
        try {
            const response = await this.axios.post<ILoginResponse>(url, loginDTO);
            console.log('getJwt response', response);
            if (response.status === 200) {
                localStorage.setItem('jwt', response.data.token)
                return response.data.token;
            }
            return null;
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return null;
        }
    }
}
