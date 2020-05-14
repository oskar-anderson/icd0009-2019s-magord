import * as JwtDecode from "jwt-decode";

export class AppState {
    constructor(){

    }
    public readonly baseUrl = 'https://localhost:5001/api/v1.0/'

    // Json Web Token to keep track of logged in status

    get jwt():string | null {
        return localStorage.getItem('jwt');
    }

    set jwt(value: string | null){
        if (value){
            localStorage.setItem('jwt', value);
        } else {
            localStorage.removeItem('jwt');
        }
    }


    get password(): string | null {
        return localStorage.getItem('password')
    }

    set password(value: string | null) {
        if (value) {
            localStorage.setItem('password', value);
        } else {
            localStorage.removeItem('password');
        }
    }


    get email():string | null {
        return localStorage.getItem('email')
    }

    set email(value: string | null) {
        if (value) {
            localStorage.setItem('email', value);
        } else {
            localStorage.removeItem('email');
        }
    }

    get isAdmin(): boolean {
        if (this.jwt) {
            const decoded = JwtDecode(this.jwt) as Record<string, string>;
            let userRole = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
            if (userRole.includes('Admin')) {
                userRole = 'Admin';
            }
            if(userRole === 'Admin') {
                return true;
            }
            return false;
        }
        return false;
    }
}
