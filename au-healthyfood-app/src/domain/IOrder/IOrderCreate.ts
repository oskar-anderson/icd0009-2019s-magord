export interface IOrderCreate {
    number: number;
    orderStatus: string;
    timeCreated: string;
    completed: boolean;

    restaurantId: string | null;

    orderTypeId: string | null;

    paymentTypeId: string | null;

}
