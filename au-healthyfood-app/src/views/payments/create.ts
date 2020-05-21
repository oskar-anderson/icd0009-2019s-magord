import { BillService } from './../../service/bill-service';
import { PaymentTypeService } from './../../service/paymenttype-service';
import { IPaymentType } from './../../domain/IPaymentType/IPaymentType';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

import { IPaymentCreate } from './../../domain/IPayment/IPaymentCreate';
import { PaymentService } from './../../service/Payment-service';
import { IBill } from 'domain/IBill/IBill';


@autoinject
export class PaymentsCreate {

    private _alert: IAlertData | null = null;

    payment: IPaymentCreate | null = null;
    _bills: IBill[] | null = null;
    _paymentTypes: IPaymentType[] | null = null;


    constructor(private billService: BillService, private router: Router, private paymentTypeService: PaymentTypeService,
         private paymentService: PaymentService) {
    }

    attached() {
        this.billService.getBills()
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    console.log({ response: response.data! });
                    this._alert = null;
                    this._bills = response.data!;
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
        this.paymentTypeService.getPaymentTypes()
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    console.log({ response: response.data! });
                    this._alert = null;
                    this._paymentTypes = response.data!;
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
        this.payment!.amount = Number(this.payment!.amount)
        this.payment!.timeMade = "12";
        this.paymentService
            .createPayment(this.payment!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('payments-index', {});
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
