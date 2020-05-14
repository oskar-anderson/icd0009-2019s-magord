import { IngredientService } from './../../service/ingredient-service';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IIngredient } from 'domain/IIngredient/IIngredient';


@autoinject
export class IngredientsDetails {

    private _alert: IAlertData | null = null;

    private ingredient?: IIngredient;

    constructor(private ingredientService: IngredientService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.ingredientService.getIngredient(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.ingredient = response.data!;
                    } else {
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.errorMessage,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                        this.ingredient = undefined;
                    }
                }                
            );
        }
    }
}
