import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';
import * as JwtDecode from 'jwt-decode';

@autoinject
export class AccountManageEmail {
    
    private _email: string = "";
    private _newEmail: string = ""
    private _errorMessage: string | null = null;

    constructor(private accountService: AccountService, private appState: AppState, private router: Router){
        
    }

    attached() {
        //this.getUserEmail();
        this._email = this.appState.email as string;
    }

    /*
    getUserEmail(): string{
        if (this.appState.jwt) {
            const decoded = JwtDecode(this.appState.jwt) as Record<string, string>;
            let userEmail = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
            this._email = userEmail;
        }
        return "";
    } 
    */   

    onSubmit(event: Event){
        console.log(this._email, this._newEmail);
        event.preventDefault();

        this.accountService.changeEmail(this._email, this._newEmail).then(
            response => {
                if (response.statusCode == 200) {
                    this.appState.jwt = response.data!.token;
                    //this.getUserEmail();
                    this.appState.email = this._newEmail;
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
