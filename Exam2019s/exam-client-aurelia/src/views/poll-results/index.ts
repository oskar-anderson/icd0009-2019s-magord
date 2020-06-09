import { QuizService } from './../../service/quiz-service';
import { IQuiz } from './../../domain/IQuiz/IQuiz';
import { IQuestion } from './../../domain/IQuestion/IQuestion';
import { ChoiceService } from './../../service/choice-service';
import { IChoice } from './../../domain/IChoice/IChoice';
import { QuestionService } from './../../service/question-service';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AppState } from 'state/app-state';
import { IAlertData } from '../../../types/IAlertData';
import { AlertType } from '../../../types/AlertType';
import { ResultService } from 'service/result-service';

@autoinject
export class QuestionsIndex {
    private _alert: IAlertData | null = null;

    private _questions: IQuestion[] = [];
    private _choices: IChoice[] = [];
    private nrOfQuestion = 0;
    private quizId: string | null = null
    private quiz: IQuiz | null = null;




    private isAdmin: boolean = false;

    constructor(private resultService: ResultService, private quizService: QuizService, private choiceService: ChoiceService,
        private questionService: QuestionService, private appState: AppState, private router: Router) {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.quizId = params.id;
            this.setQuiz(params.id)
            this.questionService.getAllForQuiz(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this._questions = response.data!;
                        for (const question of this._questions) {
                            this.nrOfQuestion += 1;
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

    setQuiz(quizId: string) {
        this.quizService.getQuiz(quizId).then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._alert = null;
                    this.quiz = response.data!;
                } else {
                    // show error message
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            }
        )
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
}
