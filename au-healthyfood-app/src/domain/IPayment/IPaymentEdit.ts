export interface IPaymentEdit {
    id: string;
    amount: number;
    timeMade: string;

    personId: string | null;
    person: string;

    billId: string | null;
    bill: number;
    
    paymentTypeId: string | null;
    paymentType: string;
}
