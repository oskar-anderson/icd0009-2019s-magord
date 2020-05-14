import { Router, RouteConfig, NavigationInstruction } from 'aurelia-router';
import { DrinkService } from './../../service/drink-service';
import { autoinject } from 'aurelia-framework';
import { AlertType } from '../../types/AlertType';
import { IAlertData } from 'types/IAlertData';
import { IDrink } from 'domain/IDrink/IDrink';
import { AppState } from 'state/app-state';

@autoinject
export class DrinksIndex {
    private _drinks: IDrink[] = [];

    private _alert: IAlertData | null = null;

    private isAdmin: boolean = false;

    constructor(private drinkService: DrinkService, private appState: AppState, private router: Router) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    attached() {
        this.drinkService.getDrinks().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this.isAdmin = this.appState.isAdmin
                    this._alert = null;
                    this._drinks = response.data!;
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

    deleteOnClick(drink: IDrink) {
        console.log("Delete")
        this.drinkService
        .deleteDrink(drink.id)
        .then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this.attached();
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
