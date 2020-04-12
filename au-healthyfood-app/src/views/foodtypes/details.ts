import { FoodTypeService } from './../../service/foodType-service';
import { IFoodType } from './../../domain/IFoodType/IFoodType';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';


@autoinject
export class FoodTypesDetails {

    private _alert: IAlertData | null = null;

    private _foodType?: IFoodType | null = null;

    constructor(private foodTypeService: FoodTypeService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.foodTypeService.getFoodType(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._foodType = response.data!;
                    } else {
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.errorMessage,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                        this._foodType = undefined;
                    }
                }                
            );
        }
    }
}
