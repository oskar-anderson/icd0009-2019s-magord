import { autoinject, bindable } from 'aurelia-framework';
import { IAlertData } from '../../types/IAlertData';

@autoinject
export class Alert {

    @bindable public alertData: IAlertData | null = null;

    constructor() {
    }
}
