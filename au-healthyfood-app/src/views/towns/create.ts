import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { TownService } from 'service/town-service';
import { ITown } from 'domain/ITown';

@autoinject
export class TownsCreate {

    _name = "";

    constructor(private townService: TownService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    onSubmit(event: Event) {
        console.log(event);
        this.townService
            .createTown({ name: this._name, areaCount: 0, id: '' })
            .then((resp) => {
                console.log('redirect?', resp);
                this.router.navigateToRoute('towns-index', {});
            });

        event.preventDefault();
    }

}
