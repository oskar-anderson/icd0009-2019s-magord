import { IQuestionEdit } from './../../domain/IQuestion/IQuestionEdit';
import { QuestionService } from './../../service/question-service';
import { QuizService } from '../../service/quiz-service';
import { IQuizEdit } from '../../domain/IQuiz/IQuizEdit';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from '../../../types/IAlertData';
import { AlertType } from '../../../types/AlertType';
import { IQuiz } from 'domain/IQuiz/IQuiz';


@autoinject
export class QuizzesEdit {

    private _alert: IAlertData | null = null;

    private question: IQuestionEdit | null = null;
    private quiz: IQuiz | null = null

    constructor(private quizService: QuizService, private questionService: QuestionService, private router: Router) {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.questionService.getQuestion(params.id).then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        console.log(response.data)
                        this.question = response.data!;
                        this.quizService.getQuiz(this.question.quizId).then(
                            response => {
                                if (response.statusCode >= 200 && response.statusCode < 300) {
                                    this._alert = null;
                                    console.log(response.data)
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
        if (this.question.description.length < 1) {
            alert("Please enter a question!")
            return null;
        }
        if(this.quiz.totalPoints !== null) {
            this.question.points = Number(this.question.points)
        }
        this.questionService
            .updateQuestion(this.question!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.navigateBack();
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
