import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http'
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { DataService } from './shared/dataService';
import { CartComponent } from './cart/cart.component';
import { ShopComponent } from './shop/shop.component';
import { ProductList } from './productList/productList.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { LoginComponent } from './login/login.component';
import { SessionStorageService } from './shared/sessionStorageService';
import { LoginpartialComponent } from './loginpartial/loginpartial.component';

let routes = [
    { path: "", component: ShopComponent},
    { path: "checkout", component: CheckoutComponent },
    { path: "login", component: LoginComponent },
];

@NgModule({
  declarations: [
      AppComponent,
      ProductList,
      CartComponent,
      ShopComponent,
      CheckoutComponent,
      LoginComponent,
      LoginpartialComponent
  ],
  imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      RouterModule.forRoot(routes, {
          useHash: true,
          enableTracing: false // for Debugging of the Routes
      })
  ],
  providers: [
      DataService,
      SessionStorageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
