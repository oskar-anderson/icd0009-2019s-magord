import { OrderTypeService } from '../../service/ordertype-service';
import { IOrderType } from '../../domain/IOrderType/IOrderType';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { AlertType } from '../../types/AlertType';
import { IAlertData } from 'types/IAlertData';
import { autoinject } from 'aurelia-framework';
import { AppState } from 'state/app-state';

@autoinject
export class OrderTypesIndex {
    private _alert: IAlertData | null = null;

    private _orderTypes: IOrderType[] = [];

    private isAdmin: boolean = false;

    constructor(private orderTypeService: OrderTypeService, private appState: AppState) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }


    attached() {
        this.orderTypeService.getOrderTypes().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this.isAdmin = this.appState.isAdmin
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

    deleteOnClick(orderType: IOrderType) {
        this.orderTypeService
        .deleteOrderType(orderType.id)
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
