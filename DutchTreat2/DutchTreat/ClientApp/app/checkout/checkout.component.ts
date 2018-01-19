import { Component } from "@angular/core";
import { Router } from '@angular/router';
import { DataService } from '../shared/dataService';

@Component({
    selector: "checkout",
    templateUrl: "checkout.component.html",
    styleUrls: ['checkout.component.css']
})
export class CheckoutComponent {

    constructor(public dataService: DataService, public router: Router) {
    }

    errorMessage: string = "";

    onCheckout() {
        // TODO
        //alert("Doing checkout");

        this.dataService.checkout()
            .subscribe(s => {
                if (s) {
                    this.router.navigate(["/"]);
                }
            }, err => this.errorMessage = "Failed to checkout");
    }
}