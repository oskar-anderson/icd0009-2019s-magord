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

    constructor(private accountService: AccountService, private appState: AppState, private router: Router){
        
    }

    attached() {
        // this.oldPassword = this.accountService.userPassword;
        this.oldPassword = this.appState.password as string;
        this.oldPasswordConfirm = this.appState.password as string;
        this.email = this.appState.email as string;
    }

    onSubmit(event: Event){
        console.log(this.oldPassword, this.newPassword)
        event.preventDefault();

        if (this.oldPassword == this.newPassword) {
            alert("New password can't be the same as current!")
            return null;
        }

        if (this.oldPassword !== this.oldPasswordConfirm) {
            alert("You have entered the wrong current password!")
            return null;
        }

        if (this.newPassword !== this.confirmPassword) {
            alert("Passwords don't match!")
            return null;
        }

        this.accountService.changePassword(this.email ,this.oldPassword, this.newPassword).then(
            response => {
                if (response.statusCode == 200) {
                    this.appState.jwt = response.data!.token;
                    this.appState.password = this.newPassword;
                    this.newPassword = "";
                    this.confirmPassword = "";
                    this.attached();
                    alert("Password changed!")
                } else {
                    this._errorMessage = response.statusCode.toString()
                        + ' ' 
                        + response.errorMessage!
                }
            }
        );
    }
    
}
