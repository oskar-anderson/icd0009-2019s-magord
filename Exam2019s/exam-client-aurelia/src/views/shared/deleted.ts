import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';

@autoinject
export class QuestionsIndex {

    constructor(private router: Router) {
    }

    navigateBack(){
        this.router.navigateBack();
    }
}
