import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { Order, OrderItem } from "./order";
import { Product } from "./product";
import { SessionStorageService } from "./sessionStorageService";

@Injectable()
export class DataService {

    private userName: string = "";
    private email: string = "";
    private token: string = "";
    private tokenExpiration: Date;

    public order: Order = new Order();

    public products: Product[] = [];

    constructor(private http: HttpClient, private storageService: SessionStorageService) {
        // get from storage
        var strg = this.storageService.getStorage();
        if (strg != null) {
            this.token = strg.token;
            this.tokenExpiration = strg.tokenExpiration;
            this.order.items = strg.orderItems;
        }
    }

    

    public get loginRequired(): boolean {
        return this.token.length == 0 || this.tokenExpiration > new Date();
    }

    public login(creds) {
        return this.http.post('/account/createtoken', creds)
            .map(response => {
                //let tokenInfo = response.json();
                let tokenInfo = response as any;
                this.token = tokenInfo.token;
                this.tokenExpiration = tokenInfo.expiration;
                this.userName = tokenInfo.userName;
                this.email = tokenInfo.email;
                // set to storage
                this.storageService.setStorageLogin(this.userName, this.token, this.tokenExpiration);
                
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
        })
            .map(res => {
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