import { ITown } from './../../domain/ITown';
import { TownService } from './../../service/town-service';
import { autoinject } from 'aurelia-framework';

@autoinject
export class TownsIndex {
    private _towns: ITown[] = [];

    constructor(private townService: TownService) {

    }

    attached() {
        this.townService.getTowns().then(
            data => this._towns = data
        );
    }


}
