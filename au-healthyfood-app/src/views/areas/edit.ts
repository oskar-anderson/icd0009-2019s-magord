import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { AreaService } from 'service/area-service';
import { IArea } from 'domain/IArea';

@autoinject
export class AreasEdit {

    private _area: IArea | null = null;

    constructor(private areaService: AreaService, private router: Router) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        console.log(params);
        if (params.id && typeof (params.id) == 'string') {
            this.areaService.getArea(params.id).then(
                data => this._area = data
            );
        }
    }

    onSubmit(event: Event) {
        console.log(event);
        this.areaService
            .updateArea(this._area!)
            .then((resp) => {
                console.log('redirect?', resp);
                this.router.navigateToRoute('areas-index', {});
            });

        event.preventDefault();
    }
}
