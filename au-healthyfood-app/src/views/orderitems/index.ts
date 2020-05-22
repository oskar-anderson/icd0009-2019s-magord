import { PaymentTypeService } from './../../service/paymenttype-service';
import { IPaymentType } from './../../domain/IPaymentType/IPaymentType';
import { IOrder } from 'domain/IOrder/IOrder';
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
    private _paymentTypes: IPaymentType[] = [];
    private orderType: string = "";

    private order: IOrder | null = null;

    private totalSum = 0;

    private firstName = "";
    private address = "";
    private phoneNr = "";

    private isAdmin: boolean = false;

    constructor(private orderItemService: OrderItemService, private appState: AppState, private router: Router,
        private paymentTypeService: PaymentTypeService) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.orderItemService.getAllForOrder(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
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
        }
    }

    attached() {
        this.orderItemService.getOrderItems().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this.isAdmin = this.appState.isAdmin
                    this._alert = null;
                    this._orderItems = response.data!;
                    for (const orderItem of this._orderItems) {
                        this.orderType = orderItem.orderType
                    }
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
        this.paymentTypeService.getPaymentTypes().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this.isAdmin = this.appState.isAdmin
                    this._alert = null;
                    this._paymentTypes = response.data!;
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
        this.firstName = this.appState.firstName as string;
        this.address = this.appState.address as string;
        this.phoneNr = this.appState.phoneNr as string
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

    addressIsSet() {
        if(this.appState.address == null){
            console.log("aaa" + this.orderType)
            return false;
            
        }
        return true;
    }

    phoneNrIsSet() {
        if(this.appState.phoneNr == null){
            return false;
        }
        return true;
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
