import { PaymentTypeService } from './../../service/Paymenttype-service';
import { IPaymentType } from './../../domain/IPaymentType/IPaymentType';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';


@autoinject
export class PaymentTypesDetails {

    private _alert: IAlertData | null = null;

    private paymentType?: IPaymentType;

    constructor(private paymentTypeService: PaymentTypeService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.paymentTypeService.getPaymentType(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.paymentType = response.data!;
                    } else {
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.errorMessage,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                        this.paymentType = undefined;
                    }
                }                
            );
        }
    }
}
