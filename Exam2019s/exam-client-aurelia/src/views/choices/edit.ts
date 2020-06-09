import { IQuestion } from './../../domain/IQuestion/IQuestion';
import { ChoiceService } from './../../service/choice-service';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from '../../../types/IAlertData';
import { AlertType } from '../../../types/AlertType';
import { IChoiceEdit } from 'domain/IChoice/IChoiceEdit';
import { IQuiz } from 'domain/IQuiz/IQuiz';
import { QuizService } from 'service/quiz-service';
import { QuestionService } from 'service/question-service';


@autoinject
export class ChoicesEdit {

    private _alert: IAlertData | null = null;

    private choice: IChoiceEdit | null = null;
    private question: IQuestion | null = null;
    private quiz: IQuiz | null = null;

    constructor(private questionService: QuestionService, private quizService: QuizService, private choiceService: ChoiceService, private router: Router) {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.choiceId && typeof (params.choiceId) == 'string') {
            this.choiceService.getChoice(params.choiceId).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.choice = response.data!;
                        this.questionService.getQuestion(this.choice.questionId).then(
                            response => {
                                if (response.statusCode >= 200 && response.statusCode < 300) {
                                    this._alert = null;
                                    this.question = response.data!;
                                    this.quizService.getQuiz(this.question.quizId).then(
                                        response => {
                                            if (response.statusCode >= 200 && response.statusCode < 300) {
                                                this._alert = null;
                                                this.quiz = response.data!;
                                            }
                                            else {
                                                this._alert = {
                                                    message: response.statusCode.toString() + ' - ' + response.errorMessage,
                                                    type: AlertType.Danger,
                                                    dismissable: true,
                                                }
                                            }
                                        }
                                    )
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

    navigateBack() {
        this.router.navigateBack();
    }


    onSubmit(event: Event) {
        event.preventDefault();
        this.choiceService
            .updateChoice(this.choice)
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
