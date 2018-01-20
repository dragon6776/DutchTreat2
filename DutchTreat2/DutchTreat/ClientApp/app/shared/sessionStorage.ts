import { OrderItem } from "./order";

export class SessionStorage {
    userName: string = "";
    token: string = "";
    tokenExpiration: Date;
    orderItems: Array<OrderItem> = new Array<OrderItem>();
}