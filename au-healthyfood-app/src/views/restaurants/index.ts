import { RestaurantService } from './../../service/restaurant-service';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { AlertType } from './../../types/AlertType';
import { IAlertData } from 'types/IAlertData';
import { AreaService } from './../../service/area-service';
import { autoinject } from 'aurelia-framework';
import { AppState } from 'state/app-state';
import { IRestaurant } from 'domain/IRestaurant/IRestaurant';

@autoinject
export class RestaurantsIndex {
    private _alert: IAlertData | null = null;

    private _restaurants: IRestaurant[] = [];

    private isAdmin: boolean = false;

    constructor(private restaurantService: RestaurantService, private appState: AppState, private router: Router) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }


    attached() {
        this.restaurantService.getRestaurants().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this.isAdmin = this.appState.isAdmin
                    this._alert = null;
                    this._restaurants = response.data!;
                    console.log(response.data)
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

    deleteOnClick(restaurant: IRestaurant) {
        console.log("Delete")
        this.restaurantService
        .deleteRestaurant(restaurant.id)
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
