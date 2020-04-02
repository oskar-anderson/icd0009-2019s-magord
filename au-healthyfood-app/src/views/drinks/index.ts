import { DrinkService } from './../../service/drink-service';
import { autoinject } from 'aurelia-framework';
import { AlertType } from '../../types/AlertType';
import { IAlertData } from 'types/IAlertData';
import { IDrink } from 'domain/IDrink/IDrink';

@autoinject
export class DrinksIndex {
    private _drinks: IDrink[] = [];

    private _alert: IAlertData | null = null;

    constructor(private drinkService: DrinkService) {

    }

    attached() {
        this.drinkService.getDrinks().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
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


}
