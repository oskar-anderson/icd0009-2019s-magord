export interface IOrderEdit {
    id: string;
    number: number;
    orderStatus: string;
    timeCreated: string;

    restaurantId: string | null;
    restaurant: string;

    orderTypeId: string  | null;
    orderType: string;

    personId: string | null;
    person: string;
}
