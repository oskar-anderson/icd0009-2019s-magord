import { OrderService } from './../../service/order-service';
import { PaymentTypeService } from '../../service/paymenttype-service';
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
    private _modalAlert: IAlertData | null = null;

    private _orderItems: IOrderItem[] = [];
    private _paymentTypes: IPaymentType[] = [];
    private orderType: string = "";
    private pickUpOrder: boolean | null = null;
    private deliveryAddress: string = "";

    private orderId: string | null = null;
    private order: IOrder | null = null

    private selectedPaymentType: string = "";
    private restaurantAddress: string = "";

    private totalSum = 0;

    private phoneNumber = "";

    private isAdmin: boolean = false;

    constructor(private orderItemService: OrderItemService, private appState: AppState, private router: Router,
        private paymentTypeService: PaymentTypeService, private orderService: OrderService) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            console.log(params.id)
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
                    this._modalAlert = null;
                    this._orderItems = response.data!;
                    for (const orderItem of this._orderItems) {
                        this.orderType = orderItem.orderType
                        this.selectedPaymentType = orderItem.paymentType;
                        this.restaurantAddress = orderItem.restaurant;
                        this.orderId = orderItem.orderId
                        if (this.orderType === "Pick-up order") {
                            this.pickUpOrder = true;
                        }
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
        this.phoneNumber = this.appState.phoneNumber as string;
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
                if (item.quantity != 1)
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

    completeOrder(orderId: string) {
        this.orderService.getOrder(orderId)
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this.order = response.data!;
                    this.order.completed = true;
                    this.order.orderStatus = "Order accepted";
                    this.updateOrder();
                }
                else {
                    // show error message
                    alert("a")
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            })
    }

    updateOrder() {
        this.orderService.updateOrder(this.order!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this.deleteAllOrderItems();
                    }
                    else {
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.errorMessage,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                    }
                }
            )
    }

    deleteAllOrderItems() {
        this.orderItemService.deleteAllOrderItems()
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this.router.navigateToRoute('orderitems-checkout')
                }
                else {
                    // show error message
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            })
    }

    showAddressError() {
        this._modalAlert = {
            message: "Please enter your delivery address!",
            type: AlertType.Danger,
            dismissable: true,
        }
    }


    onSubmit() {
        this._modalAlert = null;

        if(!this.pickUpOrder && this.deliveryAddress.length < 1 && this.selectedPaymentType == "By transfer") {
            this.showAddressError();
        }

        if((this.selectedPaymentType == 'By transfer' && this.pickUpOrder) || (this.selectedPaymentType == 'By transfer' && !this.pickUpOrder)) {
            return null;
        }

        if(this.pickUpOrder) {
            this.completeOrder(this.orderId!);
        }

        if(!this.pickUpOrder){
            if(this.deliveryAddress.length > 0) {
                this.completeOrder(this.orderId!);
            }
            else {
                this.showAddressError();
            }
        }
    }
}
