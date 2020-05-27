import { PriceService } from './../../service/price-service';
import { FoodTypeService } from '../../service/foodtype-service';
import { FoodService } from './../../service/food-service';
import { IPrice } from './../../domain/IPrice/IPrice';
import { IFoodType } from './../../domain/IFoodType/IFoodType';
import { IFoodEdit } from './../../domain/IFood/IFoodEdit';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { AreaService } from 'service/area-service';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';


@autoinject
export class FoodsEdit {

    private _alert: IAlertData | null = null;

    private food: IFoodEdit | null = null;
    private _foodTypes: IFoodType[] | null = null;
    private _prices: IPrice[] | null = null;

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
        if (params.id && typeof (params.id) == 'string') {
            this.foodService.getFood(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        console.log(response.data)
                        this.food = response.data!;
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
        event.preventDefault();
        this.food!.amount = Number(this.food!.amount);
        this.foodService
            .updateFood(this.food!)
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
