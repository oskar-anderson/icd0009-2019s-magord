import { OrderItemService } from './../../service/orderitem-service';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IOrderItem } from 'domain/IOrderItem/IOrderItem';


@autoinject
export class OrderItemsDetails {

    private _alert: IAlertData | null = null;

    private orderItem?: IOrderItem;

    constructor(private orderItemService: OrderItemService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.orderItemService.getOrderItem(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.orderItem = response.data!;
                    } else {
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.errorMessage,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                        this.orderItem = undefined;
                    }
                }                
            );
        }
    }
}
