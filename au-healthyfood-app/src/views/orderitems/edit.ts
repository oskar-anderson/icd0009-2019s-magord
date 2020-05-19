import { IngredientService } from './../../service/ingredient-service';
import { IOrderEdit } from './../../domain/IOrder/IOrderEdit';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IRestaurant } from 'domain/IRestaurant/IRestaurant';
import { IOrderType } from 'domain/IOrderType/IOrderType';
import { IPerson } from 'domain/IPerson/IPerson';
import { OrderService } from 'service/order-service';
import { OrderTypeService } from 'service/orderType-service';
import { PersonService } from 'service/person-service';
import { RestaurantService } from 'service/restaurant-service';
import { IOrderItemEdit } from 'domain/IOrderItem/IOrderItemEdit';
import { IFood } from 'domain/IFood/IFood';
import { IDrink } from 'domain/IDrink/IDrink';
import { IIngredient } from 'domain/IIngredient/IIngredient';
import { IOrder } from 'domain/IOrder/IOrder';
import { OrderItemService } from 'service/orderitem-service';
import { DrinkService } from 'service/drink-service';
import { FoodService } from 'service/food-service';


@autoinject
export class OrderItemsEdit {

    private _alert: IAlertData | null = null;

    orderItem: IOrderItemEdit | null = null;
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
        if (params.id && typeof (params.id) == 'string') {
            this.orderItemService.getOrderItem(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        console.log(response.data)
                        this.orderItem = response.data!;
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


    onSubmit(event: Event) {
        this.orderItem!.quantity = Number(this.orderItem!.quantity)
        event.preventDefault();
        this.orderItemService
            .updateOrderItem(this.orderItem!)
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
