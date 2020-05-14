import { ContactTypeService } from './../../service/contacttype-service';
import { IContactType } from './../../domain/IContactType/IContactType';
import { IContactEdit } from './../../domain/IContact/IContactEdit';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { ContactService } from 'service/contact-service';


@autoinject
export class ContactsEdit {

    private _alert: IAlertData | null = null;

    private contact: IContactEdit | null = null;
    private _contactTypes: IContactType[] | null = null;

    constructor(private contactService: ContactService, private router: Router, private contactTypeService: ContactTypeService) {
    }

    attached() {
        this.contactTypeService.getContactTypes()
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
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
        if (params.id && typeof (params.id) == 'string') {
            this.contactService.getContact(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        console.log(response.data)
                        this.contact = response.data!;
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


    onSubmit(event: Event) {
        event.preventDefault();
        if (this.contact!.contactTypeId == null || this.contact!.name == null ) {
            this._alert = {
                message: "Please choose a contact type",
                type: AlertType.Danger,
                dismissable:false,
            }
            return null;
        }
        this.contactService
            .updateContact(this.contact!)
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
        event.preventDefault();
    }



}
