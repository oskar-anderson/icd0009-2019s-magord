import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';

@autoinject
export class AccountManage {
    
    private _email: string= "";
    private _firstName: string= "";
    private _lastName: string= "";
    private _errorMessage: string | null = null;

    constructor(private accountService: AccountService, private appState: AppState){
        
    }

    attached(){
        this._email = this.appState.email as string;
        this._firstName = this.appState.firstName as string;
        this._lastName = this.appState.lastName as string;
    }

    onSubmit(event: Event) {
        event.preventDefault
        this.accountService.changeNames(this._email, this._firstName, this._lastName).then(
            response => {
                if (response.statusCode == 200) {
                    this.appState.jwt = response.data!.token;
                    this.appState.firstName = response.data!.firstName;
                    this.appState.lastName = response.data!.lastName;
                    alert("Names updated!")
                    this.attached();
                } else {
                    this._errorMessage = response.statusCode.toString()
                        + ' ' 
                        + response.errorMessage!
                }
            }
        );
    }
}
