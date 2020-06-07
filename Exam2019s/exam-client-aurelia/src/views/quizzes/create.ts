import { QuestionService } from './../../service/question-service';
import { IQuestionCreate } from './../../domain/IQuestion/IQuestionCreate';
import { QuizService } from './../../service/quiz-service';
import { IQuizCreate } from './../../domain/IQuiz/IQuizCreate';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from '../../../types/IAlertData';
import { AlertType } from '../../../types/AlertType';
import { IQuiz } from 'domain/IQuiz/IQuiz';



@autoinject
export class QuizzesCreate {

    private _alert: IAlertData | null = null;

    private quiz: IQuizCreate | null = null;


    constructor(private quizService: QuizService, private router: Router, private questionService: QuestionService) {
    }


    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    }

    onSubmit(event: Event) {
        event.preventDefault();
        this.quiz.totalPoints = Number(this.quiz.totalPoints);
        this.quizService
            .createQuiz(this.quiz!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('home', {});
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
