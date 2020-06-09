export interface IChoice {
    id: string;
    value: string;
    isSelected: boolean;
    isAnswer: boolean;
    numberOfAnswers?: number;
    
    questionId: string;
    question: string
}
