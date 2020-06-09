import { IAlertData } from '../../../types/IAlertData';
import { AppState } from 'state/app-state';
import { autoinject } from 'aurelia-framework';
import { Router, RouteConfig, NavigationInstruction } from 'aurelia-router';
import { IQuiz } from 'domain/IQuiz/IQuiz';
import { QuizService } from 'service/quiz-service';
import { AlertType } from '../../../types/AlertType';
import * as $ from 'jquery'

@autoinject
export class PollsIndex {

    private _alert: IAlertData | null = null;

    private _quizzes: IQuiz[] = [];

    private isAdmin: boolean = false;


    constructor(private quizService: QuizService, private appState: AppState, private router: Router) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {

    }


    attached() {
        this.quizService.getQuizzes().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this.isAdmin = this.appState.isAdmin
                    this._alert = null;
                    this._quizzes = response.data!;
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

        $(function () {
            $('[data-toggle="tooltip"]').tooltip()
        })
    }

    deleteOnClick(quiz: IQuiz) {
        this.quizService
            .deleteQuiz(quiz.id)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.attached();
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
