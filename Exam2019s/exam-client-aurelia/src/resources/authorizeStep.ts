import { autoinject } from 'aurelia-framework';
import { NavigationInstruction, Redirect, Next } from 'aurelia-router';
import { AppState } from "state/app-state";

@autoinject
export class AuthorizeStep {

    constructor(private appState: AppState) {
    }
    
    public run(navigationInstruction: NavigationInstruction, next: Next): Promise<any> {
        if (navigationInstruction.getAllInstructions().some(i => i.config.settings.roles.indexOf('admin') !== -1)) {
            var isAdmin = this.appState.isAdmin;
            if (!isAdmin) {
                return next.cancel(new Redirect('welcome'));
            }
        }

        return next();
    }
}
