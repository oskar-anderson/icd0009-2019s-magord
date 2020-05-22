import { IngredientService } from 'service/ingredient-service';
import { IFood } from './../../domain/IFood/IFood';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { AlertType } from './../../types/AlertType';
import { IAlertData } from 'types/IAlertData';
import { autoinject } from 'aurelia-framework';
import { AppState } from 'state/app-state';
import { FoodService } from 'service/food-service';
import { IIngredient } from 'domain/IIngredient/IIngredient';
import { OrderService } from 'service/order-service';
import { IOrderItemCreate } from 'domain/IOrderItem/IOrderItemCreate';
import { OrderItemService } from 'service/orderitem-service';
import { IOrder } from 'domain/IOrder/IOrder';

@autoinject
export class FoodsIndex {
    private _foods: IFood[] = [];
    private _ingredients: IIngredient[] = [];

    private _alert: IAlertData | null = null;

    private orderItem: IOrderItemCreate | null = null;
    private _orders: IOrder[] = []

    private ordersEmpty: boolean = false;
    private selectedOrderId: string | null = null;

    private orderItemFoods: string[] = []

    private isInCart: boolean | null = false;

    private correctAmount: boolean = true;

    private selectedIngredients: string[] | null = [];

    private isAdmin: boolean = false;

    constructor(private foodService: FoodService, private appState: AppState, private router: Router,
        private ingredientService: IngredientService, private orderService: OrderService
        , private orderItemService: OrderItemService) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }


    attached() {
        this.foodService.getFoods().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this.isAdmin = this.appState.isAdmin
                    this._alert = null;
                    this._foods = response.data!;
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
        this.ingredientService.getIngredients().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this.isAdmin = this.appState.isAdmin
                    this._alert = null;
                    this._ingredients = response.data!;
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

                    this._orders = response.data!;

                    for (const order of this._orders) {
                        if(order.completed === false) {
                            this.selectedOrderId = order.id
                            break;
                        }
                    }

                    if (this._orders.length < 1) {
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
        this.orderItemService.getOrderItems().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this.isAdmin = this.appState.isAdmin
                    this._alert = null;
                    response.data!.forEach(item => {
                        this.orderItemFoods!.push(item.food!)
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
    }

    decrement(id: string) {
        for (const item of this._foods) {
            if (item.id === id) {
                if(item.amount != 1)
                    --item.amount;
            }
        };
    }

    increment(id: string) {
        for (const item of this._foods) {
            if (item.id === id) {
                ++item.amount;
            }
        };
    }

    deleteOnClick(food: IFood) {
        console.log("Delete")
        this.foodService
            .deleteFood(food.id)
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

    displayOrderError(): void {
        if (this._orders.length < 1) {
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

    itemInCart(food: IFood) {
        for (let orderItemFood of this.orderItemFoods) {
            if (food.name === orderItemFood) {
                this.displayCartItemError();
                this.isInCart = true;
                break;
            }
            this.isInCart = false;
        }
    }

    rightAmount(food: IFood): null | void {
        if(food.amount < 1) {
            this.correctAmount = false;
            this.displayWrongAmountError();
        } else {
            this.correctAmount = true;
        }
    }

    addToCart(food: IFood) {
        this.itemInCart(food);
        this.rightAmount(food);

        food.amount = Number(food.amount);

        this.orderItem = {
            quantity: food.amount,
            drinkId: null,
            foodId: food.id,
            ingredientId: this.selectedIngredients![0],
            orderId: this.selectedOrderId
        };

        if (this.ordersEmpty === false && this.isInCart === false && this.correctAmount === true)
        {
            this.orderItemService
            .createOrderItem(this.orderItem!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        console.log("added")
                        this._alert = {
                            message: food.name + " added to cart",
                            type: AlertType.Success,
                            dismissable: true,
                        }
                        this.orderItemFoods.push(food.name)
                        this.isInCart = null;
                        this.selectedIngredients = [];
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
        this.selectedIngredients = [];
    }
}
