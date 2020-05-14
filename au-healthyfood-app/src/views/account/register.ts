import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';

@autoinject
export class AccountRegister {

    private _email: string = "";
    private _password: string = "";
    private _confirmPassword: string = "";
    private _errorMessage: string | null = null;


    constructor(private accountService: AccountService, private appState: AppState, private router: Router) {

    }

    onSubmit(event: Event) {

        if (this._password !== this._confirmPassword) {
            alert("Passwords don't match!")
            return null;
        }

        if (!(this.checkForValidPassword(this._password))) {
            return ""
        }

        console.log(this._email, this._password);
        event.preventDefault();

        this.accountService.register(this._email, this._password).then(
            response => {
                console.log(response);
                if (response.statusCode == 200) {
                    this.appState.jwt = response.data!.token;
                    this.appState.password = this._password;
                    this.appState.email = this._email;
                    this.router!.navigateToRoute('home');
                } else {
                    this._errorMessage = response.statusCode.toString()
                        + ' REGISTER FAILED '
                        + response.errorMessage!
                }
            }
        );
    }

    checkForValidPassword(password: string) {

        let answer = true;

        if (password.length < 6) {
            alert("Password is too short!")
            answer = false;
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
            alert("Password must include lowercase letter!")
            answer = false;
        }

        if (upperCounter == 0) {
            alert("Password must include uppercase letter!")
            answer = false;
        }

        let isDigit = /\d/.test(password)

        if(isDigit == false) {
            alert("Password must include a digit!")
            answer = false;
        }

        let isAlphanumeric = /^[a-z0-9]+$/i.test(password)

        if(isAlphanumeric !== false) {
            alert("Passowrd must include a nonalphanumeric symbol!")
            answer = false;
        }

        return answer;
    }
}
