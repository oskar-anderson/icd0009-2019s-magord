import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { PersonService } from 'service/person-service';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';


@autoinject
export class TownsCreate {

    private _alert: IAlertData | null = null;

    _firstName = "";
    _lastName = "";
    _sex = "";
    _dateOfBirth = "";

    constructor(private personService: PersonService, private router: Router) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }

    onSubmit(event: Event) {
        console.log(event);
        this.personService
        .createPerson({ firstName: this._firstName, lastName: this._lastName, sex: this._sex, dateOfBirth: this._dateOfBirth })
        .then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this.router.navigateToRoute('persons-index', {});
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


        event.preventDefault();
    }

}
