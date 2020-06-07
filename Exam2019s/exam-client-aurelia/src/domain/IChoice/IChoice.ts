export interface IChoice {
    id: string;
    value: string;
    isSelected: boolean;
    isAnswer: boolean;
    
    questionId: string;
    question: string
}
