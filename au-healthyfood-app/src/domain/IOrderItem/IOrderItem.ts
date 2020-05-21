export interface IOrderItem {
    id: string;
    quantity: number;

    foodId: string | null;
    food: string | null;
    foodPrice: number;

    priceId: string;

    drinkId: string | null;
    drink: string | null;
    drinkPrice: number;

    ingredientId: string | null;
    ingredient: string | null;
    ingredientPrice: number;

    orderId: string | null;
    order: string | null;
}
