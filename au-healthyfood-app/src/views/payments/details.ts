import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IPayment } from 'domain/IPayment/IPayment';
import { PaymentService } from 'service/payment-service';


@autoinject
export class PaymentDetails {

    private _alert: IAlertData | null = null;

    private payment?: IPayment;

    constructor(private paymentService: PaymentService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.paymentService.getPayment(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.payment = response.data!;
                    } else {
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.errorMessage,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                        this.payment = undefined;
                    }
                }                
            );
        }
    }
}
