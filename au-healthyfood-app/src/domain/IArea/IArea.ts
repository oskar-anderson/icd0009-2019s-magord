import { ITown } from "../ITown/ITown"

export interface IArea {
    id: string;
    name: string;
    restaurantCount: number;

    townId: string;
    town: ITown;
}
