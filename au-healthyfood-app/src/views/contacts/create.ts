import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

import { ContactService } from 'service/contact-service';
import { IContactCreate } from 'domain/IContact/IContactCreate';

import { ContactTypeService } from './../../service/contacttype-service';
import { IContactType } from './../../domain/IContactType/IContactType';



@autoinject
export class ContactsCreate {

    private _alert: IAlertData | null = null;

    contact: IContactCreate | null = null;
    _contactTypes: IContactType[] | null = null;


    constructor(private contactService: ContactService, private router: Router, private contactTypeService: ContactTypeService) {
    }

    attached() {
        this.contactTypeService.getContactTypes()
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    console.log({ response: response.data! });
                    this._alert = null;
                    this._contactTypes = response.data!;
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

    onSubmit(event: Event) {
        event.preventDefault();
        if (this.contact!.contactTypeId == null || this.contact!.name == null ) {
            this._alert = {
                message: "Please fill all the cells",
                type: AlertType.Danger,
                dismissable:false,
            }
            return null;
        }
        this.contactService
            .createContact(this.contact!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('contacts-index', {});
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
