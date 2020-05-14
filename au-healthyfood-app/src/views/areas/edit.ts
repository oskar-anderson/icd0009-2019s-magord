import { IAreaEdit } from './../../domain/IArea/IAreaEdit';
import { TownService } from './../../service/town-service';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { AreaService } from 'service/area-service';
import { IArea } from 'domain/IArea/IArea';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { ITown } from 'domain/ITown/ITown';


@autoinject
export class AreasEdit {

    private _alert: IAlertData | null = null;

    private area: IAreaEdit | null = null;
    private _towns: ITown[] | null = null;

    constructor(private areaService: AreaService, private router: Router, private townService: TownService) {
    }

    attached() {
        this.townService.getTowns()
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
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
        if (params.id && typeof (params.id) == 'string') {
            this.areaService.getArea(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        console.log(response.data)
                        this.area = response.data!;
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
            .updateArea(this.area!)
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
        event.preventDefault();
    }



}
