import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';

@autoinject
export class AccountManagePhoneNumber {
    
    private _email: string= "";
    private _phoneNumber: string = "";
    private _newPhoneNumber: string = "";
    private _errorMessage: string | null = null;
    private _successMessage: string | null = null;

    constructor(private accountService: AccountService, private appState: AppState, private router: Router){
        
    }

    attached() {
        this._phoneNumber = this.appState.phoneNumber as string;
        this._email = this.appState.email as string;
    }

    checkForValidPhoneNumber(phoneNumber: string) {
        if (/^\d{7,}$/.test(phoneNumber)) {
            return true;
        } else {
            this._errorMessage = "Please enter a valid phone number!"
            return false;
        }
    }

    onSubmit(event: Event){
        event.preventDefault();

        if(this.checkForValidPhoneNumber(this._newPhoneNumber)){
            this.accountService.changePhoneNumber(this._email, this._phoneNumber, this._newPhoneNumber).then(
                response => {
                    if (response.statusCode == 200) {
                        this._errorMessage = null
                        this.appState.jwt = response.data!.token;
                        this.appState.phoneNumber = this._newPhoneNumber;
                        this._newPhoneNumber = "";
                        this.attached();
                        this._successMessage = "Phone number successfully changed!"
                    } else {
                        this._errorMessage = response.statusCode.toString()
                            + ' ' 
                            + response.errorMessage!
                    }
                }
            );
        }
    }
}
