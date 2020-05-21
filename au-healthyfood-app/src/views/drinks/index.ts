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
    private orders: IOrder[] = []

    private ordersEmpty: boolean = false;
    private selectedOrderId: string | null = null;

    private orderItemDrinks: string[] = []

    private isInCart: boolean | null = false;

    private correctAmount: boolean = true;

    private isAdmin: boolean = false;

    constructor(private drinkService: DrinkService, private appState: AppState, private router: Router,
         private orderItemService: OrderItemService, private orderService: OrderService) {

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
                    response.data!.forEach(item => {
                        this.orderItemDrinks!.push(item.drink!)
                    });
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
        this.orderService.getOrders().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this.isAdmin = this.appState.isAdmin
                    this._alert = null;
                    this.orders = response.data!;

                    if (this.orders.length < 1) {
                        this.ordersEmpty = true;
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
        );
    }

    decrement(id: string) {
        for (const item of this._drinks) {
            if (item.id === id) {
                if(item.amount != 1)
                    --item.amount;
            }
        };
    }

    increment(id: string) {
        for (const item of this._drinks) {
            if (item.id === id) {
                ++item.amount;
            }
        };
    }

    displayOrderError(): void {
        if (this.orders.length < 1) {
            this._alert = {
                message: "Uh oh! It looks like you dont have an order created yet! Please go and create one!",
                type: AlertType.Warning,
                dismissable: true,
            }
        }
    }

    displayCartItemError(): void {
        if (this.isInCart = true) {
            this._alert = {
                message: "Oopsie! That item is already in your cart!",
                type: AlertType.Warning,
                dismissable: true,
            }
        }
    }

    displayWrongAmountError(): void {
        if (this.isInCart = true) {
            this._alert = {
                message: "Ouch! Please enter a correct amount for the item!",
                type: AlertType.Danger,
                dismissable: true,
            }
        }
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

    itemInCart(drink: IDrink) {
        for (let orderItemDrink of this.orderItemDrinks) {
            if (drink.name === orderItemDrink) {
                this.displayCartItemError();
                this.isInCart = true;
                break;
            }
            this.isInCart = false;
        }
    }

    rightAmount(drink: IDrink) {
        if(drink.amount < 1) {
            this.correctAmount = false;
            this.displayWrongAmountError();
        } else {
            this.correctAmount = true;
        }
    }
    

    addToCart(drink: IDrink): null | void {
        this.itemInCart(drink);
        this.rightAmount(drink);

        drink.amount = Number(drink.amount);
    
        this.orderItem = {
            quantity: drink.amount,
            drinkId: drink.id,
            foodId: null,
            ingredientId: null,
            orderId: this.selectedOrderId,
        };

        console.log(this.isInCart);

        if (this.ordersEmpty === false && this.isInCart === false && this.correctAmount === true)
        {
            this.orderItemService
            .createOrderItem(this.orderItem!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        console.log("added")
                        this._alert = {
                            message: drink.name + " added to cart",
                            type: AlertType.Success,
                            dismissable: true,
                        }
                        this.orderItemDrinks.push(drink.name)
                        this.isInCart = null;
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
        this.displayOrderError();
    }
}
