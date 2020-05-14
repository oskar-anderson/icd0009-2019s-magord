export interface IFood {
    id: string;
    name: string;
    description?: string;
    amount: number;
    size: string;

    foodTypeId: string;
    foodType: string

    priceId: string;
    price: number
}
