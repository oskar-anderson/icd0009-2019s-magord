export interface IOrderItemCreate {
    quantity: number;

    foodId: string | null;

    drinkId: string | null;

    ingredientId: string | null;

    orderId: string | null;
}
