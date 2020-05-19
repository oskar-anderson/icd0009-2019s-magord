import { OrderService } from './../../service/order-service';
import { IOrder } from './../../domain/IOrder/IOrder';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { AlertType } from './../../types/AlertType';
import { IAlertData } from 'types/IAlertData';
import { IArea } from '../../domain/IArea/IArea';
import { AreaService } from './../../service/area-service';
import { autoinject } from 'aurelia-framework';
import { AppState } from 'state/app-state';

@autoinject
export class OrdersIndex {
    private _alert: IAlertData | null = null;

    private _orders: IOrder[] = [];

    private isAdmin: boolean = false;

    constructor(private orderService: OrderService, private appState: AppState, private router: Router) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }


    attached() {
        this.orderService.getOrders().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this.isAdmin = this.appState.isAdmin
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

    deleteOnClick(order: IOrder) {
        console.log("Delete")
        this.orderService
        .deleteOrder(order.id)
        .then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this.attached();
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
