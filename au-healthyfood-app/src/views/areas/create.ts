import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { AreaService } from 'service/area-service';
import { IArea } from 'domain/IArea';

@autoinject
export class OwnersCreate {

    _name = ""

    constructor(private areaService: AreaService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    onSubmit(event: Event) {
        console.log(event);
        this.areaService
            .createArea({ name: this._name, restaurantCount: 0, id: '' })
            .then((resp) => {
                console.log('redirect?', resp);
                this.router.navigateToRoute('areas-index', {});
            });

        event.preventDefault();
    }

}
