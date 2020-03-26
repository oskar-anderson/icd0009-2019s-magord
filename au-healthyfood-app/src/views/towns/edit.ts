import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { TownService } from 'service/town-service';
import { ITown } from 'domain/ITown';

@autoinject
export class TownsEdit {

    private _town: ITown | null = null;

    constructor(private townService: TownService, private router: Router) {
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

    onSubmit(event: Event) {
        console.log(event);
        this.townService
            .updateTown(this._town!)
            .then((resp) => {
                console.log('redirect?', resp);
                this.router.navigateToRoute('towns-index', {});
            });

        event.preventDefault();
    }
}
