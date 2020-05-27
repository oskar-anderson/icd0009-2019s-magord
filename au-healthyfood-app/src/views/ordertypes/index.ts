import { OrderTypeService } from '../../service/ordertype-service';
import { IOrderType } from '../../domain/IOrderType/IOrderType';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { AlertType } from '../../types/AlertType';
import { IAlertData } from 'types/IAlertData';
import { autoinject } from 'aurelia-framework';

@autoinject
export class OrderTypesIndex {
    private _alert: IAlertData | null = null;

    private _orderTypes: IOrderType[] = [];

    constructor(private orderTypeService: OrderTypeService) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }


    attached() {
        this.orderTypeService.getOrderTypes().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._orderTypes = response.data!;
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
