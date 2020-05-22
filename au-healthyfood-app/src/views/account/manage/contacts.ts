import { IContactCreate } from 'domain/IContact/IContactCreate';
import { ContactService } from 'service/contact-service';
import { Router, RouteConfig, NavigationInstruction } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AppState } from 'state/app-state';
import { AlertType } from 'types/AlertType';
import { IAlertData } from 'types/IAlertData';
import { IContact } from 'domain/IContact/IContact';

@autoinject
export class AccountManageContacts {
    
    private contact: IContactCreate | null = null
    private _errorMessage: string | null = null;

    private _contacts: IContact[] = [];
    private _alert: IAlertData | null = null;

    constructor(private contactService: ContactService, private appState: AppState, private router: Router){
        
    }

    attached() {
        this.contactService.getContacts().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._contacts = response.data!;
                } else {
                    // show error message
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            }
        );
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    deleteOnClick(contact: IContact) {
        console.log("Delete")
        this.contactService
        .deleteContact(contact.id)
        .then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this.attached();
                } else {
                    // show error message
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            }
        );
    }
}
