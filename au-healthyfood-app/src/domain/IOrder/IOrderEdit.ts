export interface IOrderEdit {
    id: string;
    number: number;
    orderStatus: string;
    timeCreated: string;
    completed: boolean;

    restaurantId: string | null;
    restaurant: string;

    orderTypeId: string  | null;
    orderType: string;

    paymentTypeId: string;
    paymentType: string;
}
