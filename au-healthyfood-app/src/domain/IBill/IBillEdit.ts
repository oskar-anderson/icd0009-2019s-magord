export interface IBillEdit {
    id: string;
    timeIssued: string;
    number: number;
    sum: number;

    orderId: string | null;
    order: number;

    personId: string | null;
    person: string;
}
