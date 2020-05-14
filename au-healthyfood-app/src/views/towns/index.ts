import { ITown } from '../../domain/ITown/ITown';
import { TownService } from './../../service/town-service';
import { autoinject } from 'aurelia-framework';
import { AlertType } from './../../types/AlertType';
import { IAlertData } from 'types/IAlertData';
import { AppState } from 'state/app-state';

@autoinject
export class TownsIndex {
    private _towns: ITown[] = [];

    private isAdmin: boolean = false;

    private _alert: IAlertData | null = null;

    constructor(private townService: TownService, private appState: AppState) {
    }

    attached() {
        this.townService.getTowns().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this.isAdmin = this.appState.isAdmin
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
