import { IQuestion } from './../../domain/IQuestion/IQuestion';
import { QuestionService } from './../../service/question-service';
import { ChoiceService } from './../../service/choice-service';
import { IChoiceCreate } from './../../domain/IChoice/IChoiceCreate';

import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from '../../../types/IAlertData';
import { AlertType } from '../../../types/AlertType';



@autoinject
export class ChoicesCreate {

    private _alert: IAlertData | null = null;

    private choice: IChoiceCreate | null = null;
    private questionId: string | null = null
    private question: IQuestion | null = null;


    constructor(private questionService: QuestionService, private choiceService: ChoiceService, private router: Router) {
    }


    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.questionId = params.id
            this.questionService.getQuestion(this.questionId).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        console.log(response.data)
                        this.question = response.data!;
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

    navigateBack(){
        this.router.navigateBack();
    }

    onSubmit(event: Event) {
        event.preventDefault();
        if(this.choice.isAnswer == undefined) {
            alert("Please choose if the answer is correct or not!")
            return null;
        }
        if (this.choice.value.length < 1) {
            alert("Please enter an answer!");
            return null;
        }
        this.choice.isSelected = false;
        this.choice.questionId = this.questionId;
        this.choice.isAnswer = Boolean(this.choice.isAnswer)
        console.log(this.choice)
        this.choiceService
            .createChoice(this.choice!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateBack();
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
