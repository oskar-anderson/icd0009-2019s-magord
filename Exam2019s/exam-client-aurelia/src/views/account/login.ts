import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';

@autoinject
export class AccountLogin {
    
    private _email: string = "";
    private _password: string = "";
    private _errorMessage: string | null = null;

    constructor(private accountService: AccountService, private appState: AppState, private router: Router){
        
    }

    onSubmit(event: Event){
        console.log(this._email, this._password);
        event.preventDefault();

        this.accountService.login(this._email, this._password).then(
            response => {
                if (response.statusCode == 200) {
                    this.appState.jwt = response.data!.token;
                    this.appState.password = this._password;
                    this.appState.email = this._email;
                    this.appState.firstName = response.data!.firstName;
                    this.appState.lastName = response.data!.lastName;
                    this.router!.navigateToRoute('home');
                } else {
                    this._errorMessage = response.statusCode.toString()
                        + '- LOGIN FAILED - ' 
                        + response.errorMessage!
                }
            }
        );
    }
}
