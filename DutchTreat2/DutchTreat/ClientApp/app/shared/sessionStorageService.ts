import { Injectable } from "@angular/core";
import { SessionStorage } from "./sessionStorage";


@Injectable()
export class SessionStorageService {
    constructor() { }

    setStorage(data): any {
        let jsText = JSON.stringify(data);
        sessionStorage.setItem('dataStorage', jsText);
    }

    public setOrderItemsStorage(orderItems: any[]): any {
        let storage = this.getStorage() as SessionStorage;
        storage.orderItems = orderItems;
        this.setStorage(storage);
    }

    public getStorage(): any {
        if (sessionStorage.getItem('dataStorage')) {
            return JSON.parse(sessionStorage.getItem('dataStorage')) as SessionStorage;
        }
        return new SessionStorage();
    }

    public setStorageLogin(userName: string, token: string, tokenExpiration: Date):void {
        var strg = this.getStorage() as SessionStorage;
        strg.userName = userName;
        strg.token = token;
        strg.tokenExpiration = tokenExpiration;
    }

    public clearStorageLogin(): void {
        this.setStorageLogin('', '', null);
    }
}