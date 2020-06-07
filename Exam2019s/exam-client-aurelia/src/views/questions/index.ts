import { IQuestion } from './../../domain/IQuestion/IQuestion';
import { ChoiceService } from './../../service/choice-service';
import { IChoice } from './../../domain/IChoice/IChoice';
import { QuestionService } from './../../service/question-service';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AppState } from 'state/app-state';
import { IAlertData } from '../../../types/IAlertData';
import { AlertType } from '../../../types/AlertType';

@autoinject
export class QuestionsIndex {
    private _alert: IAlertData | null = null;

    private _questions: IQuestion[] = [];
    private questionId: string = null;
    private _choices: IChoice[] = [];
    private nrOfQuestion = 0;
    private quizId: string | null = null
    private quizName: string = ""

    private isAdmin: boolean = false;

    constructor(private choiceService: ChoiceService, private questionService: QuestionService, private appState: AppState, private router: Router) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.quizId = params.id;
            this.questionService.getAllForQuiz(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._questions = response.data!;
                        for (const question of this._questions) {
                            this.nrOfQuestion += 1;
                            this.quizName = question.quiz;
                        }
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



    attached() {
        this.choiceService.getChoices().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this.isAdmin = this.appState.isAdmin
                    this._alert = null;
                    this._choices = response.data!;
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


    deleteOnClick(question: IQuestion) {
        this.questionService
            .deleteQuestion(question.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('home')
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


    onSubmit(event: Event) {
        event.preventDefault();
        let nrOfSelectedChoices = 0;
        for (const choice of this._choices) {
            if (choice.isSelected) {
                nrOfSelectedChoices += 1
            }
        }
        if (nrOfSelectedChoices !== this.nrOfQuestion) {
            alert("Please answer all the questions")
            return null;
        } else {
            let nrOfCorrectAnswers = 0;

            for (const question of this._questions) {
                for (const choice of this._choices) {
                    if (question.id == choice.questionId) {
                        if(choice.isSelected && choice.isAnswer) {
                            nrOfCorrectAnswers += 1;
                        }
                    }
                }
            }
            alert("Congratulations! You answered " + nrOfCorrectAnswers + " questions out of " + this.nrOfQuestion + " right!")
            this.router.navigateToRoute("home")
        }
    }
}
