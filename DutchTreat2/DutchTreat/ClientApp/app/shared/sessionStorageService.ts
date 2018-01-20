import { Injectable } from "@angular/core";


@Injectable()
export class SessionStorageService {
    constructor() { }

    public setOrderItemsStorage(orderItems: any[]): any {
        let storage = this.getStorage();

        if (orderItems != null && typeof orderItems !== 'undefined') {
            storage.orderItems.length = 0;
            storage.orderItems = orderItems;
            sessionStorage.setItem('loginStorage', JSON.stringify(storage));
        }
    }

    setStorage(data): any {
        let jsText = JSON.stringify(data);
        sessionStorage.setItem('loginStorage', jsText);
    }

    public getStorage(): any {
        if (sessionStorage.getItem('loginStorage')) {
            return JSON.parse(sessionStorage.getItem('loginStorage'));
        }

        return null;
    }

    public setStorageLogin(token, tokenExpiration):any {
        var loginStorage: any = {
            token: token,
            tokenExpiration: tokenExpiration,
        };

        this.setStorage(loginStorage);
    }
}