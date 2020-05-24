export interface IOrder {
    id: string;
    number: number;
    orderStatus: string;
    timeCreated: string;
    completed: boolean;

    restaurantId: string;
    restaurant: string;

    areaId: string;
    area: string;

    townId: string;
    town: string;

    orderTypeId: string;
    orderType: string;

    paymentTypeId: string;
    paymentType: string;
}
