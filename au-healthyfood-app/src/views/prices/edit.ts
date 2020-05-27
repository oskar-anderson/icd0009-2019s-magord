import { CampaignService } from './../../service/campaign-service';
import { PriceService } from './../../service/price-service';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { IPriceEdit } from 'domain/IPrice/IPriceEdit';
import { ICampaign } from 'domain/ICampaign/ICampaign';
import flatpickr from 'flatpickr';


@autoinject
export class PricesEdit {

    private _alert: IAlertData | null = null;

    private price: IPriceEdit | null = null;
    private _campaigns: ICampaign[] | null = null;

    constructor(private priceService: PriceService, private router: Router, private campaignService: CampaignService) {
    }

    attached() {
        this.campaignService.getCampaigns()
            .then(response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
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

            flatpickr('#From, #To', {
                altInput: true,
                altFormat: "F j, Y",
                dateFormat: "d.m.Y"
            })
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.priceService.getPrice(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.price = response.data!;
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
        this.price!.value = Number(this.price!.value)
        event.preventDefault();
        this.priceService
            .updatePrice(this.price!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('prices-index', {});
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
