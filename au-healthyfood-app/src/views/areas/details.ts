import { IArea } from 'domain/IArea';
import { AreaService } from 'service/area-service';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';

@autoinject
export class AreasDetails {

    private _area: IArea | null = null;

    constructor(private areaService: AreaService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.areaService.getArea(params.id).then(
                data => this._area = data
            )
        }
    }


}
