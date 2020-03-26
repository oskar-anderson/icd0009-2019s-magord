import { ITown } from 'domain/ITown';
import { TownService } from 'service/town-service';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';

@autoinject
export class TownsDetails {

    private _town: ITown | null = null;

    constructor(private townService: TownService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.townService.getTown(params.id).then(
                data => this._town = data
            );
        }
    }


}
