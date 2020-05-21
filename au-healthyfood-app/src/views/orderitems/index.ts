import { AppState } from './../../state/app-state';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { AlertType } from './../../types/AlertType';
import { IAlertData } from 'types/IAlertData';
import { autoinject } from 'aurelia-framework';
import { OrderItemService } from 'service/orderitem-service';
import { IOrderItem } from 'domain/IOrderItem/IOrderItem';

@autoinject
export class OrderItemsIndex {
    private _alert: IAlertData | null = null;

    private _orderItems: IOrderItem[] = [];

    private totalSum = 0;

    private firstName = "";

    private isAdmin: boolean = false;

    constructor(private orderItemService: OrderItemService, private appState: AppState, private router: Router) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    attached() {
        this.orderItemService.getOrderItems().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this.isAdmin = this.appState.isAdmin
                    this._alert = null;
                    this._orderItems = response.data!;
                    this.calculatePrice();
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
        this.firstName = this.appState.firstName as string
    }

    calculatePrice() {
        let sum = 0;
        for (const item of this._orderItems) {
            sum += item.quantity * item.foodPrice,
                sum += item.quantity * item.drinkPrice,
                sum += item.quantity * item.ingredientPrice
        }
        this.totalSum = sum;
    }

    decrement(id: string) {
        for (const item of this._orderItems) {
            if (item.id === id) {
                if(item.quantity != 1)
                    --item.quantity;
            }
            this.calculatePrice();
        };
    }

    increment(id: string) {
        for (const item of this._orderItems) {
            if (item.id === id) {
                ++item.quantity;
            }
            this.calculatePrice();
        };
    }

    deleteOnClick(orderItem: IOrderItem) {
        console.log("Delete")
        console.log(orderItem.id)
        this.orderItemService
            .deleteOrderItem(orderItem.id)
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
