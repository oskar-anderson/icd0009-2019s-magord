import { FoodService } from './../../service/food-service';
import { PriceService } from './../../service/price-service';
import { IngredientService } from './../../service/ingredient-service';
import { IPrice } from './../../domain/IPrice/IPrice';
import { IIngredientCreate } from './../../domain/IIngredient/IIngredientCreate';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IFood } from 'domain/IFood/IFood';


@autoinject
export class IngredientsCreate {

    private _alert: IAlertData | null = null;

    ingredient: IIngredientCreate | null = null;
    _prices: IPrice[] | null = null;
    _foods: IFood[] | null = null


    constructor(private ingredientService: IngredientService, private router: Router, private priceService: PriceService, private foodService: FoodService) {
    }

    attached() {
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
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    }

    onSubmit(event: Event) {
        event.preventDefault();
        this.ingredientService
            .createIngredient(this.ingredient!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('ingredients-index', {});
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
