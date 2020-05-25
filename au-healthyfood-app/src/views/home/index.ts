import { AppState } from 'state/app-state';
import { autoinject } from 'aurelia-framework';
import { Router } from 'aurelia-router';
import { IRestaurant } from './../../domain/IRestaurant/IRestaurant';
import { IAlertData } from 'types/IAlertData';

@autoinject
export class HomeIndex {

    restaurants: IRestaurant[] | null = null;
    private _alert: IAlertData | null = null;


    constructor(private router: Router, private appState: AppState) {
    }

}
