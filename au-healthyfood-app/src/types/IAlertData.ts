import { AlertType } from "./AlertType";

export interface IAlertData {
    message: string;
    dismissable?: boolean;
    type: AlertType
}
