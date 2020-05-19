import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

import { IPaymentTypeCreate } from './../../domain/IPaymentType/IPaymentTypeCreate';
import { PaymentTypeService } from './../../service/Paymenttype-service';


@autoinject
export class PaymentTypesCreate {

    private _alert: IAlertData | null = null;

    paymentType: IPaymentTypeCreate | null = null;


    constructor(private paymentTypeService: PaymentTypeService, private router: Router) {
    }


    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    }

    onSubmit(event: Event) {
        event.preventDefault();
        this.paymentTypeService
            .createPaymentType(this.paymentType!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('paymenttypes-index', {});
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
