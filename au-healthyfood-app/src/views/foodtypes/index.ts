import { FoodTypeService } from './../../service/foodType-service';
import { IFoodType } from './../../domain/IFoodType/IFoodType';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { AlertType } from '../../types/AlertType';
import { IAlertData } from 'types/IAlertData';
import { autoinject } from 'aurelia-framework';

@autoinject
export class FoodTypesIndex {
    private _alert: IAlertData | null = null;

    private _foodTypes: IFoodType[] = [];

    constructor(private foodTypeService: FoodTypeService) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }


    attached() {
        this.foodTypeService.getFoodTypes().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
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
    }



}
