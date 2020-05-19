import { IOrderItem } from './../../domain/IOrderItem/IOrderItem';
import { IOrderCreate } from './../../domain/IOrder/IOrderCreate';
import { OrderService } from './../../service/order-service';
import { OrderItemService } from './../../service/orderitem-service';
import { Router, RouteConfig, NavigationInstruction } from 'aurelia-router';
import { DrinkService } from './../../service/drink-service';
import { autoinject } from 'aurelia-framework';
import { AlertType } from '../../types/AlertType';
import { IAlertData } from 'types/IAlertData';
import { IDrink } from 'domain/IDrink/IDrink';
import { AppState } from 'state/app-state';
import { IOrderItemCreate } from 'domain/IOrderItem/IOrderItemCreate';
import { IOrder } from 'domain/IOrder/IOrder';

@autoinject
export class DrinksIndex {
    private _drinks: IDrink[] = [];

    private _alert: IAlertData | null = null;

    private orderItem: IOrderItemCreate | null = null;
    private order: IOrderCreate | null = null;
    private orders: IOrder[] = []

    private orderItems: IOrderItem[] = []

    private isAdmin: boolean = false;

    constructor(private drinkService: DrinkService, private appState: AppState, private router: Router, private orderItemService: OrderItemService, private orderService: OrderService) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    attached() {
        this.drinkService.getDrinks().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this.isAdmin = this.appState.isAdmin
                    this._alert = null;
                    this._drinks = response.data!;
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
        this.orderItemService.getOrderItems().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this.isAdmin = this.appState.isAdmin
                    this._alert = null;
                    this.orderItems = response.data!;
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

    deleteOnClick(drink: IDrink) {
        console.log("Delete")
        this.drinkService
            .deleteDrink(drink.id)
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

    /*
    toggleToAdd(drink: IDrink) {
        this.orderItemService
            .getOrderItems()
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        for (let orderItem of response.data!) {
                            if (orderItem.drink === drink.name) {
                                this.toAdd = false;
                                alert("ITEM ALREADY IN ORDER!")
                            } else {
                                this.toAdd = true;
                            }
                        }
                    }
                });
    }
    */

    addToCart(drink: IDrink): null | void {
        /*
        for (let orderItem of this.orderItems) {
            if (orderItem.drink === drink.name) {
                this.toAdd = false;
                alert("ITEM ALREADY IN ORDER!")
            } else {
                this.toAdd = true;
            }
            console.log(this.toAdd)
        }
        */

        drink.amount = Number(drink.amount);

        this.order = {
            number: 12,
            orderStatus: "Underway",
            timeCreated: "Now",
            restaurantId: "00000000-0000-0000-0000-000000000001",
            orderTypeId: "00000000-0000-0000-0000-000000000001",
            personId: "00000000-0000-0000-0000-000000000001",

        }
        this.orderService
            .createOrder(this.order!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
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

        this.orderItem = {
            quantity: drink.amount,
            drinkId: drink.id,
            foodId: null,
            ingredientId: null,
            orderId: "00000000-0000-0000-0000-000000000001"
        }

        this.orderItemService
            .createOrderItem(this.orderItem!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        console.log("added")
                        this._alert = {
                            message: "Item added to cart",
                            type: AlertType.Success,
                            dismissable: true,
                        }
                    } else {
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
}
