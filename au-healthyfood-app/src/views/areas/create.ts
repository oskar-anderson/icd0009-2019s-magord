import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

import { AreaService } from 'service/area-service';
import { TownService } from 'service/town-service'
import { ITown } from 'domain/ITown/ITown'
import { IAreaCreate } from 'domain/IArea/IAreaCreate';


@autoinject
export class AreasCreate {

    private _alert: IAlertData | null = null;

    area: IAreaCreate | null = null;
    _towns: ITown[] | null = null;


    constructor(private areaService: AreaService, private router: Router, private townService: TownService) {
    }

    attached() {
        this.townService.getTowns()
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    console.log({ response: response.data! });
                    this._alert = null;
                    this._towns = response.data!;
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

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    }

    onSubmit(event: Event) {
        event.preventDefault();
        if (this.area!.townId == null || this.area!.name == null ) {
            this._alert = {
                message: "Please fill all the cells",
                type: AlertType.Danger,
                dismissable:false,
            }
            return null;
        }
        this.areaService
            .createArea(this.area!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('areas-index', {});
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
