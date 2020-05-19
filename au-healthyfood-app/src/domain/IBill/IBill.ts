export interface IBill {
    id: string;
    timeIssued: string;
    number: number;
    sum: number;

    orderId: string;
    order: number;

    personId: string;
    person: string;
}
