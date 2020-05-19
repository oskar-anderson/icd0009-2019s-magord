export interface IBillCreate {
    timeIssued: string;
    number: number;
    sum: number;

    orderId: string | null;

    personId: string | null;
}
