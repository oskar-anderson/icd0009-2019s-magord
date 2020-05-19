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


@autoinject
export class OrdersEdit {

    private _alert: IAlertData | null = null;

    order: IOrderEdit | null = null;
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
        if (params.id && typeof (params.id) == 'string') {
            this.orderService.getOrder(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        console.log(response.data)
                        this.order = response.data!;
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
        this.order!.number = Number(this.order!.number)
        event.preventDefault();
        this.orderService
            .updateOrder(this.order!)
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
