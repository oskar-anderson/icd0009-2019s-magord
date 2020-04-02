import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { AlertType } from './../../types/AlertType';
import { IAlertData } from 'types/IAlertData';
import { IArea } from '../../domain/IArea/IArea';
import { AreaService } from './../../service/area-service';
import { autoinject } from 'aurelia-framework';

@autoinject
export class AreasIndex {
    private _alert: IAlertData | null = null;

    private _areas: IArea[] = [];

    constructor(private areaService: AreaService) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }


    attached() {
        this.areaService.getAreas().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this._areas = response.data!;
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
