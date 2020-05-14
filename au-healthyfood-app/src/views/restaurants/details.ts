import { RestaurantService } from './../../service/restaurant-service';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IRestaurant } from 'domain/IRestaurant/IRestaurant';


@autoinject
export class RestaurantsDetails {

    private _alert: IAlertData | null = null;

    private restaurant?: IRestaurant;

    constructor(private restaurantService: RestaurantService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.restaurantService.getRestaurant(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.restaurant = response.data!;
                    } else {
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.errorMessage,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                        this.restaurant = undefined;
                    }
                }                
            );
        }
    }
}
