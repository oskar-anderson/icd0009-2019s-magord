export interface IFoodEdit {
    id: string;
    name: string;
    description?: string;
    amount: number;
    size: string;

    foodTypeId: string | null;
    foodType: string

    priceId: string | null;
    price: number
}
