export interface IPriceCreate {
    value: number;
    from: string;
    to: string;
    campaignId: string | null;
}
