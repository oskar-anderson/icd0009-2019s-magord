export class AppState {
    constructor(){

    }
    public readonly baseUrl = 'https://localhost:5001/api/'

    // Json Web Token to keep track of logged in status

    //public jwt: string | null = null;

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

}
