export interface IQuestionEdit {
    id: string;
    number: number;
    description: string;
    points?: number;
    
    quizId: string | null;
    quiz: string
}
