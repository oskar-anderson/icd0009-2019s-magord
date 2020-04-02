import { IDrinkEdit } from 'domain/IDrink/IDrinkEdit';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { DrinkService } from './../../service/drink-service';


@autoinject
export class DrinksEdit {

    private _alert: IAlertData | null = null;
    private _drink?: IDrinkEdit;

    constructor(private drinkService: DrinkService, private router: Router) {
    }

    attached() {
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
        console.log(event);

        this._drink!.amount = Number(this._drink!.amount);
        this._drink!.size = Number(this._drink!.size);

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


        event.preventDefault();
    }
}
