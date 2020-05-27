import { ICampaignEdit } from './../../domain/ICampaign/ICampaignEdit';
import { CampaignService } from './../../service/campaign-service';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import flatpickr from 'flatpickr';




@autoinject
export class CampaginsEdit {

    private _alert: IAlertData | null = null;
    private _campagin?: ICampaignEdit;

    constructor(private campaginService: CampaignService, private router: Router) {
    }

    attached() {
        flatpickr('#From, #To', {
            altInput: true,
            altFormat: "F j, Y",
            dateFormat: "d.m.Y"
        })
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.campaginService.getCampaign(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._campagin = response.data!;
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

    onSubmit(event: Event) {
        event.preventDefault();
        this.campaginService
            .updateCampaign(this._campagin!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('campaigns-index', {});
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
