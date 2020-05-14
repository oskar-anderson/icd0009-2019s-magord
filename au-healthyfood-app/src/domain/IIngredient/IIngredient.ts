export interface IIngredient {
    id: string;
    name: string;
    amount: number;

    foodId: string;
    food: string;

    priceId: string;
    price: number;
}
