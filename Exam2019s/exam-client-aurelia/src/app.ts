import { AppState } from './state/app-state';
import { autoinject, PLATFORM } from 'aurelia-framework';
import {RouterConfiguration, Router} from 'aurelia-router';
import { AuthorizeStep} from 'resources/authorizeStep'
import { IAlertData } from '../types/IAlertData';

@autoinject
export class App {
    router?: Router;
    private _alert: IAlertData | null = null;

    constructor(private appState: AppState) {

    }


    configureRouter(config: RouterConfiguration, router: Router): void {
        this.router = router;

        config.title = "Quiz-Fun";
        config.addAuthorizeStep(AuthorizeStep);

        config.map([
            { route: ['', 'home', 'home/index'], name: 'home', moduleId: PLATFORM.moduleName('views/home/index'), nav: true, title: 'Quizzes', settings: { roles: [] } },


            { route: ['account/login'], name: 'account-login', moduleId: PLATFORM.moduleName('views/account/login'), nav: false, title: 'Login', settings: { roles: [] } },
            { route: ['account/register'], name: 'account-register', moduleId: PLATFORM.moduleName('views/account/register'), nav: false, title: 'Register', settings: { roles: [] } },


            { route: ['account/manage'], name: 'account-manage', moduleId: PLATFORM.moduleName('views/account/manage/index'), nav: false, title: 'Manage account', settings: { roles: [] } },
            { route: ['account/manageEmail'], name: 'account-manageEmail', moduleId: PLATFORM.moduleName('views/account/manage/email'), nav: false, title: 'Manage email', settings: { roles: [] } },
            { route: ['account/managePassword'], name: 'account-managePassword', moduleId: PLATFORM.moduleName('views/account/manage/password'), nav: false, title: 'Manage password', settings: { roles: [] } },



            { route: ['shared/deleted'], name: 'deleted', moduleId: PLATFORM.moduleName('views/shared/deleted'), nav: false, title: 'Item deleted', settings: { roles: ['admin'] } },

            { route: ['polls/create'], name: 'polls-create', moduleId: PLATFORM.moduleName('views/polls/create'), nav: false, title: 'Polls create', settings: { roles: ['admin'] } },
            { route: ['polls/edit/:id?'], name: 'polls-edit', moduleId: PLATFORM.moduleName('views/polls/edit'), nav: false, title: 'Polls edit', settings: { roles: ['admin'] } },
            { route: ['polls', 'polls/index'], name: 'polls-index', moduleId: PLATFORM.moduleName('views/polls/index'), nav: true, title: 'Polls', settings: { roles: [] } },

            { route: ['poll-results', 'poll-results/index'], name: 'poll-results-index', moduleId: PLATFORM.moduleName('views/poll-results/index'), nav: false, title: 'Polls results', settings: { roles: [] } },

            

            { route: ['quizzes/edit/:id?'], name: 'quizzes-edit', moduleId: PLATFORM.moduleName('views/quizzes/edit'), nav: false, title: 'Quizzes Edit', settings: { roles: ['admin'] } },
            { route: ['quizzes/create'], name: 'quizzes-create', moduleId: PLATFORM.moduleName('views/quizzes/create'), nav: false, title: 'Quizzes Create', settings: { roles: ['admin'] } },

            
            { route: ['questions', 'questions/index'], name: 'questions-index', moduleId: PLATFORM.moduleName('views/questions/index'), nav: false, title: 'Questions', settings: { roles: [] } },
            { route: ['questions/edit/:id?'], name: 'questions-edit', moduleId: PLATFORM.moduleName('views/questions/edit'), nav: false, title: 'Questions Edit', settings: { roles: ['admin'] } },
            { route: ['questions/create'], name: 'questions-create', moduleId: PLATFORM.moduleName('views/questions/create'), nav: false, title: 'Questions Create', settings: { roles: ['admin'] } },

            
            { route: ['choices/edit/:id?'], name: 'choices-edit', moduleId: PLATFORM.moduleName('views/choices/edit'), nav: false, title: 'Choices Edit', settings: { roles: ['admin'] } },
            { route: ['choices/create'], name: 'choices-create', moduleId: PLATFORM.moduleName('views/choices/create'), nav: false, title: 'Choices Create', settings: { roles: ['admin'] } }
            
        ]);

        config.mapUnknownRoutes('views/home/index');
        config.fallbackRoute('views/home/index')

    }

    logoutOnClick() {
        this.appState.jwt = null;
        this.appState.email = null
        this.appState.firstName = null
        this.appState.lastName = null
        this.appState.password = null
        this.router!.navigateToRoute('home');
    }
}






