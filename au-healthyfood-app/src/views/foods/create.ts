import { PriceService } from './../../service/price-service';
import { FoodTypeService } from '../../service/foodtype-service';
import { IFoodType } from './../../domain/IFoodType/IFoodType';
import { IFoodCreate } from './../../domain/IFood/IFoodCreate';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IPrice } from 'domain/IPrice/IPrice';
import { FoodService } from 'service/food-service';


@autoinject
export class FoodCreate {

    private _alert: IAlertData | null = null;

    food: IFoodCreate | null = null;
    _foodTypes: IFoodType[] | null = null;
    _prices: IPrice[] | null = null;


    constructor(private foodService: FoodService, private router: Router, private foodTypeService: FoodTypeService, private priceService: PriceService) {
    }

    attached() {
        this.foodTypeService.getFoodTypes()
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    console.log({ response: response.data! });
                    this._alert = null;
                    this._foodTypes = response.data!;
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

        this.priceService.getPrices()
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    console.log({ response: response.data! });
                    this._alert = null;
                    this._prices = response.data!;
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
        this.food!.amount = Number(this.food!.amount);
        this.foodService
            .createFood(this.food!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('foods-index', {});
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
