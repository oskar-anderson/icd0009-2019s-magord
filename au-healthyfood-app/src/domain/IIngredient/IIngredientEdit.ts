export interface IIngredientEdit {
    id: string;
    name: string;

    foodId: string | null;
    food: string;

    priceId: string | null;
    price: number;
}
