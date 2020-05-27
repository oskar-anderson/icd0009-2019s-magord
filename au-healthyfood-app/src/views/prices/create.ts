import { CampaignService } from './../../service/campaign-service';
import { PriceService } from './../../service/price-service';
import { IPriceCreate } from './../../domain/IPrice/IPriceCreate';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import { ICampaign } from 'domain/ICampaign/ICampaign';
import flatpickr from 'flatpickr';


@autoinject
export class PricesCreate {

    private _alert: IAlertData | null = null;

    price: IPriceCreate | null = null;
    _campaigns: ICampaign[] | null = null;


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
    }

    onSubmit(event: Event) {
        this.price!.value = Number(this.price!.value)
        event.preventDefault();
        this.priceService
            .createPrice(this.price!)
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
