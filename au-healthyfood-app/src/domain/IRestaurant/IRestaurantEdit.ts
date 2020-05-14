export interface IRestaurantEdit {
    id: string;
    name: string;
    address: string;
    openedFrom: string;
    closedFrom: string;
    
    areaId: string | null;
    area: string
}
