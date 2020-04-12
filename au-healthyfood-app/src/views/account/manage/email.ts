import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';

@autoinject
export class AccountManageEmail {
    
    private _email: string = "";
    private _newEmail: string = ""
    private _errorMessage: string | null = null;

    constructor(private accountService: AccountService, private appState: AppState, private router: Router){
        
    }

    attached() {
        this._email = this.accountService.userEmail;
    }

    onSubmit(event: Event){
        console.log(this._email, this._newEmail);
        event.preventDefault();

        this.accountService.changeEmail(this._email, this._newEmail).then(
            response => {
                if (response.statusCode == 200) {
                    this.appState.jwt = response.data!.token;
                    this.router!.navigateToRoute('account-manage');
                    alert("Email changed!")
                } else {
                    this._errorMessage = response.statusCode.toString()
                        + ' ' 
                        + response.errorMessage!
                        console.log(this._errorMessage);
                }
            }
        );
    }
    
}
