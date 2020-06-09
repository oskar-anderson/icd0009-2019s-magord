export interface IResultEdit {
    id: string;
    timesPlayed: number;
    totalScore?: number;
    
    quizId: string | null;
}
