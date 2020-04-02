import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

import { DrinkService } from 'service/drink-service';
import { IDrink } from 'domain/IDrink/IDrink';


@autoinject
export class DrinksDelete {
    private _alert: IAlertData | null = null;

    private _drink?: IDrink;


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
                        };
                        this._drink = undefined;
                    }
                }
            );
        }
    }

    onSubmit(event: Event) {
        this.drinkService
        .deleteDrink(this._drink!.id)
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
