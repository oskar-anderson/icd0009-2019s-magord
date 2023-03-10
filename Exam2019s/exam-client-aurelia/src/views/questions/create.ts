import { QuizService } from './../../service/quiz-service';
import { QuestionService } from './../../service/question-service';
import { IQuestionCreate } from './../../domain/IQuestion/IQuestionCreate';
import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IAlertData } from '../../../types/IAlertData';
import { AlertType } from '../../../types/AlertType';
import { IQuestion } from 'domain/IQuestion/IQuestion';
import { IQuiz } from 'domain/IQuiz/IQuiz';



@autoinject
export class QuestionsCreate {

    private _alert: IAlertData | null = null;

    private question: IQuestionCreate | null = null;

    private quizId: string | null = null
    private quiz: IQuiz | null = null

    private questions: IQuestion[] = [];


    constructor(private quizService: QuizService, private questionService: QuestionService, private router: Router) {
    }


    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if (params.id && typeof (params.id) == 'string') {
            this.quizId = params.id;
            this.quizService.getQuiz(params.id).then(
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
        }
    }

    navigateBack(){
        this.router.navigateBack();
    }

    onSubmit(event: Event) {
        event.preventDefault();
        this.question.quizId = this.quizId;
        this.question.number = 0
        if(this.question.description.length < 1) {
            alert("Please enter a question!")
            return null;
        }
        this.question.points = Number(this.question.points)
        this.questionService
            .createQuestion(this.question)
            .then(
                response => {
                    if (response.statusCode >= 200 && response.statusCode < 300) {
                        this._alert = null;
                        this.router.navigateToRoute('questions-index', {id: this.quizId});
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
