export interface IIngredientEdit {
    id: string;
    name: string;
    amount: number;

    foodId: string | null;
    food: string;

    priceId: string | null;
    price: number;
}
