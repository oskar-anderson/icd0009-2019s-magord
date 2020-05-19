export interface IOrderItemEdit {
    id: string;
    quantity: number;

    foodId: string | null;
    food: string | null;

    drinkId: string | null;
    drink: string | null;

    ingredientId: string | null;
    ingredient: string | null;

    orderId: string | null;
    order: string | null;

}
