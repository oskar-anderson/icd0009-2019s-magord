import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { OrderTypeService } from 'service/ordertype-service';



@autoinject
export class OrderTypesCreate {

    private _alert: IAlertData | null = null;

    _name = "";
    _comment = "";

    constructor(private orderTypeService: OrderTypeService, private router: Router) {
        
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    }

    onSubmit(event: Event) {
        console.log(event);
        this.orderTypeService
            .createOrderType({ name: this._name, comment: this._comment })
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('ordertypes-index', {});
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
