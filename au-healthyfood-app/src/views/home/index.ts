import { AppState } from 'state/app-state';
import { autoinject } from 'aurelia-framework';
import { Router } from 'aurelia-router';
import { RestaurantService } from './../../service/restaurant-service';
import { IRestaurant } from './../../domain/IRestaurant/IRestaurant';
import { AlertType } from 'types/AlertType';
import { IAlertData } from 'types/IAlertData';
var $ = require('jquery')

@autoinject
export class HomeIndex {

    restaurants: IRestaurant[] | null = null;
    private _alert: IAlertData | null = null;


    constructor(private restaurantService: RestaurantService, private router: Router, private appState: AppState) {
    }

    attached() {
        this.restaurantService.getRestaurants()
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    console.log({ response: response.data! });
                    this._alert = null;
                    this.restaurants = response.data!;
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
