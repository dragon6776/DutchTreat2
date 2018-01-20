import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from '../shared/dataService';

@Component({
  selector: 'the-cart',
  templateUrl: "cart.component.html",
  styles: []
})
export class CartComponent implements OnInit {

  constructor(private dataService: DataService, private router: Router) { }

  ngOnInit() {
  }

  onCheckout() {
      if (this.dataService.loginRequired) {
          // force login
          this.router.navigate(['login']);
      } else {
          // go to checkout
          this.router.navigate(['checkout']);
      }
  }
}
