import { PriceService } from './../../service/price-service';
import { IDrinkCreate } from 'domain/IDrink/IDrinkCreate';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { DrinkService } from 'service/drink-service';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IPrice } from 'domain/IPrice/IPrice';


@autoinject
export class DrinksCreate {

    private _alert: IAlertData | null = null;

    drink: IDrinkCreate | null = null;
    _prices: IPrice[] | null = null;

    constructor(private drinkService: DrinkService, private router: Router, private priceService: PriceService) {

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
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    onSubmit(event: Event) {
        this.drink!.amount = Number(this.drink!.amount);
        this.drink!.size = Number(this.drink!.size)
        event.preventDefault();
        console.log(event);
        this.drinkService
        .createDrink(this.drink!)
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
