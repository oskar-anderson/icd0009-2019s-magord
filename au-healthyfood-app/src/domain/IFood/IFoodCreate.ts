export interface IFoodCreate {
    name: string;
    description?: string;
    amount: number;
    size: string;

    foodTypeId: string | null;

    priceId: string | null;
}
