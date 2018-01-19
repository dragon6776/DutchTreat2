//import * as _ from "lodash";
// package.json: "lodash":"^2.17.4"

export class Order {
    id: number;
    orderDate: Date = new Date();
    orderNumber: string;
    items: Array<OrderItem> = new Array<OrderItem>();
    get subtotal(): number {
        if (!this.items.length)
            return 0;

        var r = this.items
            .map(x => x.quantity * x.unitPrice)
            .reduce((sum, x) => sum + x);

        return r;
    }
}

export class OrderItem {
    id: number;
    quantity: number;
    unitPrice: number;
    productId: number;
    productCategory: string;
    productTitle: string;
    productArtist: string;
    productArtId: string;
    productSize: string;
}


