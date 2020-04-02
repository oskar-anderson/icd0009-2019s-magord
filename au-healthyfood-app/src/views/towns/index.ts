import { ITown } from '../../domain/ITown/ITown';
import { TownService } from './../../service/town-service';
import { autoinject } from 'aurelia-framework';
import { AlertType } from './../../types/AlertType';
import { IAlertData } from 'types/IAlertData';

@autoinject
export class TownsIndex {
    private _towns: ITown[] = [];

    private _alert: IAlertData | null = null;

    constructor(private townService: TownService) {

    }

    attached() {
        this.townService.getTowns().then(
            response => {
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


}
