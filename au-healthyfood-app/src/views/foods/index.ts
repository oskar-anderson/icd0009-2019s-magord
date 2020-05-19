import { IngredientService } from 'service/ingredient-service';
import { IFood } from './../../domain/IFood/IFood';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { AlertType } from './../../types/AlertType';
import { IAlertData } from 'types/IAlertData';
import { autoinject } from 'aurelia-framework';
import { AppState } from 'state/app-state';
import { FoodService } from 'service/food-service';
import { IIngredient } from 'domain/IIngredient/IIngredient';
import { IOrderCreate } from 'domain/IOrder/IOrderCreate';
import { OrderService } from 'service/order-service';
import { IOrderItemCreate } from 'domain/IOrderItem/IOrderItemCreate';
import { OrderItemService } from 'service/orderitem-service';
import { IOrder } from 'domain/IOrder/IOrder';

@autoinject
export class FoodsIndex {
    private _alert: IAlertData | null = null;

    private _foods: IFood[] = [];

    private _ingredients: IIngredient[] = [];

    private ingredientsForFood: IIngredient[] = [];

    selectedIngredients: string[] | null = [];

    private order: IOrderCreate | null = null;

    private _orders: IOrder[] | null = [];

    private orderItem: IOrderItemCreate | null = null;


    private isAdmin: boolean = false;

    constructor(private foodService: FoodService, private appState: AppState, private router: Router, private ingredientService: IngredientService, private orderService: OrderService
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

    addToCart(food: IFood) {
        food.amount = Number(food.amount);

        if (this._orders!.length === 0) {
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



            this.orderItem = {
                quantity: food.amount,
                drinkId: null,
                foodId: food.id,
                ingredientId: this.selectedIngredients![0],
                orderId: this._orders![0].id
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
        } else {
            this.orderItem = {
                quantity: food.amount,
                drinkId: null,
                foodId: food.id,
                ingredientId: this.selectedIngredients![0],
                orderId: this._orders![0].id
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

        this.selectedIngredients = [];
    }
}
