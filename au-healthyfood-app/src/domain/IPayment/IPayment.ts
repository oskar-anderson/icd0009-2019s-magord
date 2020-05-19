export interface IPayment {
    id: string;
    amount: number;
    timeMade: string;

    personId: string;
    person: string;

    billId: string;
    bill: number;
    
    paymentTypeId: string;
    paymentType: string;
}
