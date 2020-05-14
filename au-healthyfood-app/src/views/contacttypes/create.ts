import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

import { IContactTypeCreate } from './../../domain/IContactType/IContactTypeCreate';
import { ContactTypeService } from './../../service/contacttype-service';


@autoinject
export class ContactTypesCreate {

    private _alert: IAlertData | null = null;

    contactType: IContactTypeCreate | null = null;


    constructor(private contactTypeService: ContactTypeService, private router: Router) {
    }


    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    }

    onSubmit(event: Event) {
        event.preventDefault();
        this.contactTypeService
            .createContactType(this.contactType!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('contacttypes-index', {});
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
