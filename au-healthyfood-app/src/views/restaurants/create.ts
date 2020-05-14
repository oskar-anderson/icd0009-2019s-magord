import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

import { IRestaurantCreate } from './../../domain/IRestaurant/IRestaurantCreate';
import { AreaService } from './../../service/area-service';
import { IArea } from 'domain/IArea/IArea';
import { RestaurantService } from 'service/restaurant-service';
import flatpickr from 'flatpickr';


@autoinject
export class RestaurantsCreate {

    private _alert: IAlertData | null = null;

    restaurant: IRestaurantCreate | null = null;
    _areas: IArea[] | null = null;


    constructor(private restaurantService: RestaurantService, private router: Router, private areaService: AreaService) {
    }

    attached() {
        this.areaService.getAreas()
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    console.log({ response: response.data! });
                    this._alert = null;
                    this._areas = response.data!;
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
        flatpickr('#OpenedFrom, #ClosedFrom', {
            enableTime: true,
            noCalendar: true,
            dateFormat: "H:i",
            time_24hr: true
        })
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    }

    onSubmit(event: Event) {
        event.preventDefault();
        if (this.restaurant!.areaId == null) {
            this._alert = {
                message: "Please fill all the cells",
                type: AlertType.Danger,
                dismissable:false,
            }
            return null;
        }
        this.restaurantService
            .createRestaurant(this.restaurant!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('restaurants-index', {});
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
