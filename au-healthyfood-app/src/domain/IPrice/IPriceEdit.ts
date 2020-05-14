export interface IPriceEdit {
    id: string;
    value: number;
    from: string;
    to: string;
    campaignId: string | null;
    campaign: string | null;
}
