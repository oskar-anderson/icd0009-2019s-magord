export interface IOrderCreate {
    number: number;
    orderStatus: string;
    timeCreated: string;

    restaurantId: string | null;

    orderTypeId: string | null;

    personId: string | null;

}
