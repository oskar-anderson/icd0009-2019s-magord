import { ContactTypeService } from './../../service/contacttype-service';
import { IContactType } from './../../domain/IContactType/IContactType';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';


@autoinject
export class ContactTypesDetails {

    private _alert: IAlertData | null = null;

    private contactType?: IContactType;

    constructor(private contactTypeService: ContactTypeService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.contactTypeService.getContactType(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.contactType = response.data!;
                    } else {
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.errorMessage,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                        this.contactType = undefined;
                    }
                }                
            );
        }
    }
}
