export interface IPaymentCreate {
    amount: number;
    timeMade: string;

    personId: string | null;

    billId: string | null;
    
    paymentTypeId: string | null;
}
