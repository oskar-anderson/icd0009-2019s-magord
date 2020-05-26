import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';

@autoinject
export class AccountManagePhoneNumber {
    
    private _email: string= "";
    private _phoneNumber: string = "";
    private _newPhoneNumber: string = ""
    private _errorMessage: string | null = null;

    constructor(private accountService: AccountService, private appState: AppState, private router: Router){
        
    }

    attached() {
        this._phoneNumber = this.appState.phoneNumber as string;
        this._email = this.appState.email as string;
    }

    onSubmit(event: Event){
        event.preventDefault();

        this.accountService.changePhoneNumber(this._email, this._phoneNumber, this._newPhoneNumber).then(
            response => {
                if (response.statusCode == 200) {
                    this.appState.jwt = response.data!.token;
                    this.appState.phoneNumber = this._newPhoneNumber;
                    this._newPhoneNumber = "";
                    this.attached();
                    alert("Phone number changed!")
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
