import { IDrinkEdit } from 'domain/IDrink/IDrinkEdit';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { DrinkService } from './../../service/drink-service';
import { IPrice } from 'domain/IPrice/IPrice';
import { PriceService } from 'service/price-service';


@autoinject
export class DrinksEdit {

    private _alert: IAlertData | null = null;
    private _drink?: IDrinkEdit;
    private _prices: IPrice[] | null = null;

    constructor(private drinkService: DrinkService, private router: Router, private priceService: PriceService) {
    }

    attached() {
        this.priceService.getPrices()
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
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
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.drinkService.getDrink(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._drink = response.data!;
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
        console.log(event);
        this._drink!.amount = Number(this._drink!.amount);
        this.drinkService
            .updateDrink(this._drink!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('drinks-index', {});
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
