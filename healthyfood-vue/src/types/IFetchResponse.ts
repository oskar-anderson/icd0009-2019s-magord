export interface IFetchResponse<TData> {
    statusCode: number;
    errorMessage?: string;
    data?: TData
}
