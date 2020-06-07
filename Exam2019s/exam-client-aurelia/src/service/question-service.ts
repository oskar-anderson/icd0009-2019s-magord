import { IQuiz } from './../domain/IQuiz/IQuiz';
import { IQuestionCreate } from './../domain/IQuestion/IQuestionCreate';
import { IQuestion } from './../domain/IQuestion/IQuestion';
import { autoinject } from 'aurelia-framework';
import { AppState } from 'state/app-state';
import { HttpClient } from 'aurelia-fetch-client';
import { IFetchResponse } from '../../types/IFetchResponse';
import { IQuestionEdit } from 'domain/IQuestion/IQuestionEdit';


@autoinject
export class QuestionService {
    constructor(private appState: AppState, private httpClient: HttpClient) {
        this.httpClient.baseUrl = this.appState.baseUrl;
    }

    private readonly _baseUrl = 'Questions';

    async getQuestions(): Promise<IFetchResponse<IQuestion[]>> {
        try {
            const response = await this.httpClient
                .fetch(this._baseUrl, {
                    cache: "no-store",
                });
            // happy case
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IQuestion[];
                console.log(data)
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            // something went wrong
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }

        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

    async getAllForQuiz(quizId: string): Promise<IFetchResponse<IQuestion[]>> {
        try {
            const response = await this.httpClient
                .fetch(this._baseUrl + '?quizId=' + quizId, {
                    cache: "no-store",
                });
            // happy case
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IQuestion[];
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            // something went wrong
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }

        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }


    async getQuestion(id: string): Promise<IFetchResponse<IQuestion>> {
        try {
            const response = await this.httpClient
                .fetch(this._baseUrl + '/' + id, {
                    cache: "no-store",
                });

            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as IQuestion;
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }

        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }


    async createQuestion(question: IQuestionCreate): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient
                .post(this._baseUrl, JSON.stringify(question), {
                    cache: 'no-store',
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                })

            if (response.status >= 200 && response.status < 300) {
                console.log('response', response);
                return {
                    statusCode: response.status
                    // no data
                }
            }

            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }
        }
        catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }


    async updateQuestion(question: IQuestionEdit): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient
                .put(this._baseUrl + '/' + question.id, JSON.stringify(question), {
                    cache: 'no-store',
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });

            if (response.status >= 200 && response.status < 300) {
                return {
                    statusCode: response.status
                    // no data
                }
            }
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }
        }
        catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }


    async deleteQuestion(id: string): Promise<IFetchResponse<string>> {
        try {
            const response = await this.httpClient
            .delete(this._baseUrl + '/' + id, null, {
                cache: 'no-store',
                headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
            });

            if (response.status >= 200 && response.status < 300) {
                return {
                    statusCode: response.status
                    // no data
                }
            }
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }
        }
        catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }
}
