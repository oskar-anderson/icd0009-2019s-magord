import { AppState } from './../../state/app-state';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';

@autoinject
export class OrderItemsCheckout {
    
    constructor(private appState: AppState, private router: Router) {
    }
    
}
