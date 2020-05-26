import { OrderItemService } from './../../service/orderitem-service';
import { OrderService } from './../../service/order-service';
import { IOrder } from './../../domain/IOrder/IOrder';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { AlertType } from './../../types/AlertType';
import { IAlertData } from 'types/IAlertData';
import { autoinject } from 'aurelia-framework';
import { AppState } from 'state/app-state';
import { IOrderItem } from 'domain/IOrderItem/IOrderItem';

@autoinject
export class OrdersIndex {
    private _alert: IAlertData | null = null;

    private _orders: IOrder[] = [];
    private _orderItems: IOrderItem[] = [];
    private _orderInProgess: boolean = false

    private isAdmin: boolean = false;

    constructor(private orderService: OrderService, private appState: AppState, private router: Router,
        private orderItemService: OrderItemService) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }


    attached() {
        this._orderInProgess = false;
        this.orderService.getOrders().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this.isAdmin = this.appState.isAdmin
                    this._alert = null;
                    this._orders = response.data!;
                    for (const order of this._orders) {
                        if(order.completed === false) {
                            this._orderInProgess = true
                            break;
                        }
                    }
                    console.log(this._orderInProgess)
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
                    this._orderItems = [];
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

    createOrder(event: Event) {
        event.preventDefault;
        if(this._orderInProgess) {
            this._alert = {
                message: "Uh oh! It looks like you already have an active order in progress! Please wait for it to get finished!",
                type: AlertType.Warning,
                dismissable: true,
            }
            return null;
        }
        this.router.navigateToRoute('orders-create', {});
    }
}
