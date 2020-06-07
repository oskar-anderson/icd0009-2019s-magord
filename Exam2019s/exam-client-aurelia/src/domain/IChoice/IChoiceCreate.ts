export interface IChoiceCreate {
    value: string;
    isSelected: boolean;
    isAnswer: boolean;
    
    questionId: string | null;
}
