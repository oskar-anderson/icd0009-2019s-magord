import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';

@autoinject
export class AccountRegister {

    private _email: string = "";
    private _password: string = "";
    private _confirmPassword: string = "";
    private _firstName: string = "";
    private _lastName: string = "";
    private _errorMessage: string | null = null;


    constructor(private accountService: AccountService, private appState: AppState, private router: Router) {

    }

    onSubmit(event: Event) {

        if (this._password !== this._confirmPassword) {
            this._errorMessage = "The passwords do not match!"
            return null;
        }

        if(this._email.length < 1 || this._firstName.length < 1 || this._lastName.length < 1) {
            this._errorMessage = "Please fill all the fields!"
            return null;
        }

        if (!(this.checkForValidPassword(this._password))) {
            return null
        }

        event.preventDefault();

        this.accountService.register(this._email, this._password, this._firstName, this._lastName).then(
            response => {
                console.log(response);
                if (response.statusCode == 200) {
                    this.appState.jwt = response.data!.token;
                    this.appState.password = this._password;
                    this.appState.email = this._email;
                    this.appState.firstName = response.data!.firstName;
                    this.appState.lastName = response.data!.lastName;
                    this.router!.navigateToRoute('home');
                } else {
                    this._errorMessage = response.statusCode.toString()
                        + ' REGISTRATION FAILED '
                        + response.errorMessage!
                }
            }
        );
    }

    checkForValidPassword(password: string) {

        let answer = true;

        if (password.length < 6) {
            this._errorMessage = "The password is too short!"
            answer = false;
            return null;
        }

        let lowerCounter = 0
        let upperCounter = 0

        for (let i = 0; i < password.length; i++) {
            const element = password[i];
            if(/\d/.test(element) == false && element == element.toLowerCase()) {
                lowerCounter++
            }
            if(/\d/.test(element) == false && element == element.toUpperCase()) {
                upperCounter++
            }
        }

        if (lowerCounter == 0) {
            this._errorMessage = "The password must include lowercase letter!"
            answer = false;
            return null;
        }

        if (upperCounter == 0) {
            this._errorMessage = "The password must include an uppercase letter!"
            answer = false;
            return null;
        }
        

        let isDigit = /\d/.test(password)

        if(isDigit == false) {
            this._errorMessage = "The password must include a digit!"
            answer = false;
            return null;
        }

        let isAlphanumeric = /^[a-z0-9]+$/i.test(password)

        if(isAlphanumeric !== false) {
            this._errorMessage = "The password must include a nonalphanumeric symbol!"
            answer = false;
            return null;
        }

        return answer;
    }
}
