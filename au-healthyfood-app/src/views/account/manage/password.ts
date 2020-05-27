import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';

@autoinject
export class AccountManagePassword {
    
    public email: string = ""
    public oldPassword: string = "";
    public oldPasswordConfirm: string = "";
    public newPassword: string = "";
    public confirmPassword: string = "";
    private _errorMessage: string | null = null;
    private _successMessage: string | null = null;

    constructor(private accountService: AccountService, private appState: AppState, private router: Router){
        
    }

    attached() {
        this.oldPassword = this.appState.password as string;
        this.oldPasswordConfirm = this.appState.password as string;
        this.email = this.appState.email as string;
    }

    onSubmit(event: Event){
        event.preventDefault();

        if (this.oldPassword == this.newPassword) {
            this._errorMessage = "New password can't be the same as current!"
            return null;
        }

        if (this.oldPassword !== this.oldPasswordConfirm) {
            this._errorMessage = "You have entered the wrong current password!"
            return null;
        }

        if (this.newPassword !== this.confirmPassword) {
            this._errorMessage = "The passwords do not match!"
            return null;
        }

        this.accountService.changePassword(this.email ,this.oldPassword, this.newPassword).then(
            response => {
                if (response.statusCode == 200) {
                    this._errorMessage = null;
                    this.appState.jwt = response.data!.token;
                    this.appState.password = this.newPassword;
                    this.newPassword = "";
                    this.confirmPassword = "";
                    this.attached();
                    this._successMessage = "The password was successfully changed!"
                } else {
                    this._errorMessage = response.statusCode.toString()
                        + ' ' 
                        + response.errorMessage!
                }
            }
        );
    }
    
}
