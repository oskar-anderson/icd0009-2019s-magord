import { ICampaignCreate } from './../../domain/ICampaign/ICampaignCreate';
import { CampaignService } from './../../service/campaign-service';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import flatpickr from 'flatpickr'
require("flatpickr/dist/flatpickr.css")


@autoinject
export class CampaignsCreate {

    private _alert: IAlertData | null = null;

    campaign: ICampaignCreate | null = null;

    constructor(private campaignService: CampaignService, private router: Router) {

    }

    attached() {
        flatpickr('#From, #To', {
            altInput: true,
            altFormat: "F j, Y",
            dateFormat: "d/m/Y"
        })
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    onSubmit(event: Event) {
        event.preventDefault();
        this.campaignService
        .createCampaign(this.campaign!)
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
