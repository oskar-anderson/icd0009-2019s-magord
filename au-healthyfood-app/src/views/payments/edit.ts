import { PaymentTypeService } from './../../service/Paymenttype-service';
import { IPaymentTypeEdit } from './../../domain/IPaymentType/IPaymentTypeEdit';
import { IPaymentType } from './../../domain/IPaymentType/IPaymentType';

import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IPerson } from 'domain/IPerson/IPerson';
import { IBill } from 'domain/IBill/IBill';
import { BillService } from 'service/bill-service';
import { PaymentService } from 'service/payment-service';
import { PersonService } from 'service/person-service';
import { IPaymentEdit } from 'domain/IPayment/IPaymentEdit';


@autoinject
export class PaymentEdit {

    private _alert: IAlertData | null = null;

    payment: IPaymentEdit | null = null;
    _persons: IPerson[] | null = null;
    _bills: IBill[] | null = null;
    _paymentTypes: IPaymentType[] | null = null;

    constructor(private billService: BillService, private router: Router, private paymentTypeService: PaymentTypeService, private personService: PersonService,
        private paymentService: PaymentService) {
   }

   attached() {
    this.personService.getPersons()
        .then(response => {
            if (response.statusCode >= 200 && response.statusCode < 300) {
                console.log({ response: response.data! });
                this._alert = null;
                this._persons = response.data!;
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
        if (params.id && typeof (params.id) == 'string') {
            this.paymentService.getPayment(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        console.log(response.data)
                        this.payment = response.data!;
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
        this.payment!.amount = Number(this.payment!.amount)
        this.paymentService
            .updatePayment(this.payment!)
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
