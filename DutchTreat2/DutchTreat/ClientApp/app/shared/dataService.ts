import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { Order, OrderItem } from "./order";
import { Product } from "./product";
import { SessionStorageService } from "./sessionStorageService";
import { SessionStorage } from "./sessionStorage";
declare var $: any;

@Injectable()
export class DataService {


    private userName: string = "";
    private email: string = "";
    private token: string = "";
    private tokenExpiration: Date;

    public order: Order = new Order();

    public products: Product[] = [];

    constructor(private http: HttpClient, private storageService: SessionStorageService) {
        // update login info from storage
        var strg = this.storageService.getStorage() as SessionStorage;
        if (strg.token !== '') {
            this.token = strg.token;
            this.tokenExpiration = strg.tokenExpiration;
            this.userName = strg.userName;
            this.email = strg.email;
        }

        // check cookie auth logged
        //if (this.cookieAuthLogged != this.jwtAuthLogged) {
        //    if (this.cookieAuthLogged) {
        //        this.setLogoutCookieAuth();
        //    } else {
        //        this.setLogoutJwtAuth();
        //    }
        //}

        if (strg.orderItems)
            this.order.items = strg.orderItems;
    }


    //setLogoutCookieAuth() {
    //    // set logout cookie on server, then updating logout status storage
    //    $('#logoutForm').submit();
    //}

    //setLogoutJwtAuth() {
    //    this.token = '';
    //    this.tokenExpiration = null;
    //    this.userName = '';
    //    this.email = '';
    //    // clear storage
    //    this.storageService.clearStorageLogin();
    //}

    //logoff() {
    //    this.setLogoutCookieAuth();
    //    // cookie logoff
    //    this.setLogoutCookieAuth();

    //    // set to storage
    //    this.storageService.clearStorageLogin();
    //}

    setJwtLoginFromTokenInfo(tokenInfo: SessionStorage): void {
        this.token = tokenInfo.token;
        this.tokenExpiration = tokenInfo.tokenExpiration;
        this.userName = tokenInfo.userName;
        this.email = tokenInfo.email;

        // set to storage
        this.storageService.setStorageLogin(this.userName, this.token, this.tokenExpiration);
    }

    public get loginRequired(): boolean {
        return this.token.length == 0 || this.tokenExpiration > new Date();
    }

    //public get cookieAuthLogged(): boolean {
    //    return $('#logoutForm').length > 0;
    //}

    //public get jwtAuthLogged(): boolean {
    //    return !this.loginRequired;
    //}

    public login(creds) {
        return this.http.post('/account/createtoken', creds)
            .map(response => {
                //let tokenInfo = response.json();
                let tokenInfo = response as SessionStorage;

                this.setJwtLoginFromTokenInfo(tokenInfo);

                return true;
            });
    }


    public checkout() {
        if (!this.order.orderNumber) {
            var d = this.order.orderDate;
            this.order.orderNumber = d.getFullYear().toString() + d.getTime().toString();
        }

        return this.http.post("/api/orders", this.order, {
            headers: new HttpHeaders({ "Authorization": "Bearer " + this.token })
        }).map(res => {
            this.order = new Order();
            return true;
        });
    }

    loadProducts() {
        return this.http.get('/api/products')
            .map((data: any[]) => {
                this.products = data;
                return true;
            });
    }

    public AddToOrder(product: Product) {

        let item: OrderItem = this.order.items.find(i => i.productId == product.id);

        if (item) {
            item.quantity++;
        } else {
            item = new OrderItem();
            item.productId = product.id;
            item.productArtId = product.artId;
            item.productArtist = product.artist;
            item.productCategory = product.category;
            item.productTitle = product.title;
            item.productSize = product.size;
            item.unitPrice = product.price;
            item.quantity = 1;
            this.order.items.push(item);
        }
        // update to storage
        this.storageService.setOrderItemsStorage(this.order.items);
    }
}