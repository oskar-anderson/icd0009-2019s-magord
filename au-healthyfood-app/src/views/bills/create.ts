import { BillService } from './../../service/bill-service';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IOrder } from 'domain/IOrder/IOrder';
import { IBillCreate } from 'domain/IBill/IBillCreate';
import { OrderService } from 'service/order-service';


@autoinject
export class BillsCreate {

    private _alert: IAlertData | null = null;

    bill: IBillCreate | null = null;
    _orders: IOrder[] | null = null;


    constructor(private billService: BillService, private router: Router, private orderService: OrderService) {
    }

    attached() {
        this.orderService.getOrders()
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    console.log({ response: response.data! });
                    this._alert = null;
                    this._orders = response.data!;
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
        this.bill!.sum = Number(this.bill!.sum);
        this.bill!.timeIssued = "12";
        this.bill!.number = 3;
        event.preventDefault();
        this.billService
            .createBill(this.bill!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('bills-index', {});
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
