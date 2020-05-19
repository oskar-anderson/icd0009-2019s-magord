export interface IOrder {
    id: string;
    number: number;
    orderStatus: string;
    timeCreated: string;

    restaurantId: string;
    restaurant: string;

    orderTypeId: string;
    orderType: string;

    personId: string;
    person: string;
}
