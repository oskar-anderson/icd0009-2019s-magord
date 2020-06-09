export interface IChoiceEdit {
    id: string;
    value: string;
    isSelected: boolean;
    isAnswer: boolean;
    numberOfAnswers?: number;
    
    questionId: string | null;
    question: string
}
