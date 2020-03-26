import { IArea } from './../../domain/IArea';
import { AreaService } from './../../service/area-service';
import { autoinject } from 'aurelia-framework';

@autoinject
export class AreasIndex {
    private _areas: IArea[] = [];

    constructor(private areaService: AreaService) {

    }

    attached() {
        this.areaService.getAreas().then(
            data => this._areas = data
        );
    }


}
