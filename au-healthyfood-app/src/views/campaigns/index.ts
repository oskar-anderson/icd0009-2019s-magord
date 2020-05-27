import { autoinject } from 'aurelia-framework';
import { AlertType } from './../../types/AlertType';
import { IAlertData } from 'types/IAlertData';
import { AppState } from 'state/app-state';
import { CampaignService } from 'service/campaign-service';
import { ICampaign } from 'domain/ICampaign/ICampaign';

@autoinject
export class CampaignsIndex {
    private _campaigns: ICampaign[] = [];

    private isAdmin: boolean = false;

    private _alert: IAlertData | null = null;

    constructor(private campaignService: CampaignService, private appState: AppState) {
    }

    attached() {
        this.campaignService.getCampaigns().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this.isAdmin = this.appState.isAdmin
                    this._alert = null;
                    this._campaigns = response.data!;
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

    deleteOnClick(campaign: ICampaign) {
        this.campaignService
        .deleteCampagin(campaign.id)
        .then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this.attached();
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
