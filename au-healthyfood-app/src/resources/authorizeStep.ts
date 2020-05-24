import { NavigationInstruction, Redirect, Next } from 'aurelia-router';
import { AppState } from "state/app-state";

export class AuthorizeStep {

    constructor(private appState: AppState) {

    }

    run = (navigationInstruction: NavigationInstruction, next: Next): Promise<any> => {
        if (navigationInstruction.getAllInstructions().some(i => i.config.settings.roles.indexOf('admin') !== -1)) {
            console.log(this.appState)
            var isAdmin = this.appState.isAdmin;
            if (!isAdmin) {
                return next.cancel(new Redirect('welcome'));
            }
        }
        return next();
    }

    /*
    public run(navigationInstruction: NavigationInstruction, next: Next): Promise<any> {
        if (navigationInstruction.getAllInstructions().some(i => i.config.settings.roles.indexOf('admin') !== -1)) {
            console.log(this.appState)
            var isAdmin = this.appState.isAdmin;
            if (!isAdmin) {
                return next.cancel(new Redirect('welcome'));
            }
        }

        return next();
    }
    */
}
