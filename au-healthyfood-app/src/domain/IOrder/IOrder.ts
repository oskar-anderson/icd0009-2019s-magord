export interface IOrder {
    id: string;
    number: number;
    orderStatus: string;
    timeCreated: string;
    completed: boolean;

    restaurantId: string;
    restaurant: string;

    orderTypeId: string;
    orderType: string;

    paymentTypeId: string;
    paymentType: string;
}
