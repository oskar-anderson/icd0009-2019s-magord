export interface IChoiceEdit {
    id: string;
    value: string;
    isSelected: boolean;
    isAnswer: boolean;
    
    questionId: string | null;
    question: string
}
