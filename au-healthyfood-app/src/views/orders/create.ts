import { PersonService } from './../../service/person-service';
import { OrderTypeService } from './../../service/orderType-service';
import { RestaurantService } from './../../service/restaurant-service';
import { OrderService } from './../../service/order-service';
import { IOrderType } from './../../domain/IOrderType/IOrderType';
import { IRestaurant } from './../../domain/IRestaurant/IRestaurant';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IOrderCreate } from 'domain/IOrder/IOrderCreate';
import { IPerson } from 'domain/IPerson/IPerson';


@autoinject
export class OrdersCreate {

    private _alert: IAlertData | null = null;

    order: IOrderCreate | null = null;
    _restaurants: IRestaurant[] | null = null;
    _orderTypes: IOrderType[] | null = null;
    _persons: IPerson[] | null = null;


    constructor(private orderService: OrderService, private router: Router, private restaurantService: RestaurantService,
         private orderTypeService: OrderTypeService, private personService: PersonService) {
    }

    attached() {
        this.restaurantService.getRestaurants()
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    console.log({ response: response.data! });
                    this._alert = null;
                    this._restaurants = response.data!;
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
        this.orderTypeService.getOrderTypes()
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    console.log({ response: response.data! });
                    this._alert = null;
                    this._orderTypes = response.data!;
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
        this.personService.getPersons()
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    console.log({ response: response.data! });
                    this._alert = null;
                    this._persons = response.data!;
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
        this.order!.number = 1;
        this.order!.orderStatus = "a";
        this.order!.timeCreated = "12"
        this.orderService
            .createOrder(this.order!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('orders-index', {});
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
