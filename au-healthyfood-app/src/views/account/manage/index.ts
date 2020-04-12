import { AccountLogin } from './../login';
import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/account-service';
import { AppState } from 'state/app-state';

@autoinject
export class AccountManage {
    
    private _email: string= "";

    constructor(private accountService: AccountService, private appState: AppState){
        
    }

    attached(){
        this._email = this.accountService.userEmail;
    }
    
}
