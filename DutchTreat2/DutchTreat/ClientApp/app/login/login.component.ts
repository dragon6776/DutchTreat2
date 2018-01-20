import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from '../shared/dataService';

@Component({
    selector: 'login',
    templateUrl: 'login.component.html',
    styles: []
})
export class LoginComponent implements OnInit {
    title: string = "Login";
    constructor(private dataService: DataService, private router: Router) { }

    ngOnInit() {
        if (!this.dataService.loginRequired) {
            this.router.navigate(['/']);
        }
    }

    errorMessage: string = "";

    public creds = {
        username: "",
        password: ""
    };


    onLogin() {
        // call the login service
        this.dataService.login(this.creds)
            .subscribe(success => {
                if (this.dataService.order.items.length == 0) {
                    this.router.navigate(["/"]);
                    // reload page for updating the header cookie logged info
                    //location.reload();
                } else {
                    this.router.navigate(["login"]);
                }
            }, err => this.errorMessage = "Failed to login");
    }
}
