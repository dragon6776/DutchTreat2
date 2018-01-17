import { Component, OnInit } from "@angular/core";
import { DataService } from "../shared/dataService";


@Component({
    selector: "product-list",
    templateUrl: "productList.component.html",
    styleUrls: []
})

export class ProductList implements OnInit {
    public products = [];

    constructor(private dataService: DataService) {
    }


    ngOnInit(): void {
        this.dataService.loadProducts().subscribe(s => {
            if (s) {
                this.products = this.dataService.products;
            }
        });
    }

}