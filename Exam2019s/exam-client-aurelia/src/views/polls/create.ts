import { QuestionService } from './../../service/question-service';
import { QuizService } from './../../service/quiz-service';
import { IQuizCreate } from './../../domain/IQuiz/IQuizCreate';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from '../../../types/IAlertData';
import { AlertType } from '../../../types/AlertType';



@autoinject
export class PollsCreate {

    private _alert: IAlertData | null = null;

    private poll: IQuizCreate | null = null;


    constructor(private quizService: QuizService, private router: Router, private questionService: QuestionService) {
    }


    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    }

    onSubmit(event: Event) {
        event.preventDefault();
        if(this.poll.name.length < 1) {
            alert("Please enter a name for the poll!")
            return null;
        }
        this.quizService
            .createQuiz(this.poll!)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('polls-index', {});
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
