export interface IIngredientCreate {
    name: string;
    amount: number;

    foodId: string | null;

    priceId: string | null;
}
