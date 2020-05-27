import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';

@autoinject
export class AccountManageEmail {

    private _email: string = "";
    private _newEmail: string = ""
    private _errorMessage: string | null = null;
    private _successMessage: string | null = null;

    constructor(private accountService: AccountService, private appState: AppState, private router: Router) {

    }

    attached() {
        this._email = this.appState.email as string;
    }

    onSubmit(event: Event) {
        event.preventDefault();

        this.accountService.changeEmail(this._email, this._newEmail).then(
            response => {
                if (response.statusCode == 200) {
                    this._errorMessage = null;
                    this.appState.jwt = response.data!.token;
                    this.appState.email = this._newEmail;
                    this._newEmail = "";
                    this.attached();
                    this._successMessage = "Your email was successfully changed!"
                } else {
                    this._errorMessage = response.statusCode.toString()
                        + ' '
                        + response.errorMessage!
                }
            }
        );
    }

}
