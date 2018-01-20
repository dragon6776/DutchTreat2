import { Component, OnInit } from "@angular/core";
import { Router } from '@angular/router';
import { DataService } from '../shared/dataService';

@Component({
    selector: "checkout",
    templateUrl: "checkout.component.html",
    styleUrls: ['checkout.component.css']
})
export class CheckoutComponent implements OnInit {
    ngOnInit(): void {
        if (this.dataService.loginRequired) {
            // force login
            this.router.navigate(['login']);
        } else {
            // go to checkout
            this.router.navigate(['checkout']);
        }
    }

    constructor(public dataService: DataService, public router: Router) {
    }

    errorMessage: string = "";

    onCheckout() {
        this.dataService.checkout()
            .subscribe(s => {
                if (s) {
                    this.router.navigate(["/"]);
                }
            }, err => this.errorMessage = "Failed to checkout");
    }
}