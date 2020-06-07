export interface IQuestionCreate {
    number: number;
    description: string;
    points: number;
    
    quizId: string | null;
}
