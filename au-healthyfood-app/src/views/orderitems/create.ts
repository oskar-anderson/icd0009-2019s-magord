import { OrderService } from 'service/order-service';
import { IngredientService } from './../../service/ingredient-service';
import { DrinkService } from './../../service/drink-service';
import { FoodService } from './../../service/food-service';
import { IOrder } from './../../domain/IOrder/IOrder';
import { IDrink } from './../../domain/IDrink/IDrink';
import { IFood } from './../../domain/IFood/IFood';
import { OrderItemService } from './../../service/orderitem-service';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IOrderItemCreate } from 'domain/IOrderItem/IOrderItemCreate';
import { IIngredient } from 'domain/IIngredient/IIngredient';


@autoinject
export class OrderItemsCreate {

    private _alert: IAlertData | null = null;

    orderItem: IOrderItemCreate | null = null;
    _foods: IFood[] | null = null;
    _drinks: IDrink[] | null = null;
    _ingredients: IIngredient[] | null = null;
    _orders: IOrder[] | null = null;


    constructor(private orderItemService: OrderItemService, private router: Router, private foodService: FoodService,
         private drinkService: DrinkService, private ingredientService: IngredientService, private orderService: OrderService) {
    }

    attached() {
        this.foodService.getFoods()
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    console.log({ response: response.data! });
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
        this.drinkService.getDrinks()
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    console.log({ response: response.data! });
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
        this.ingredientService.getIngredients()
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    console.log({ response: response.data! });
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
        event.preventDefault();
        this.orderItem!.quantity = Number(this.orderItem!.quantity)
        this.orderItemService
            .createOrderItem(this.orderItem!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('orderitems-index', {});
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
