import { IOrder } from './../../domain/IOrder/IOrder';
import { IArea } from 'domain/IArea/IArea';
import { AreaService } from 'service/area-service';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { OrderService } from 'service/order-service';


@autoinject
export class OrdersDetails {

    private _alert: IAlertData | null = null;

    private order?: IOrder;

    constructor(private orderService: OrderService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.orderService.getOrder(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.order = response.data!;
                    } else {
                        // show error message
                        this._alert = {
                            message: response.statusCode.toString() + ' - ' + response.errorMessage,
                            type: AlertType.Danger,
                            dismissable: true,
                        }
                        this.order = undefined;
                    }
                }                
            );
        }
    }
}
