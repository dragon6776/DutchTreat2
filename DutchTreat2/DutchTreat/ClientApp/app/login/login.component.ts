import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from '../shared/dataService';

@Component({
    selector: 'login',
    templateUrl: 'login.component.html',
    styles: []
})
export class LoginComponent implements OnInit {

    constructor(private dataService: DataService, private router: Router) { }

    ngOnInit() {
    }

    errorMessage: string = "";

    public creds = {
        username: "",
        password: ""
    };


    onLogin() {
        // call the login service
        //alert(this.creds.username);
        debugger;
        this.dataService.login(this.creds)
            .subscribe(success => {
                if (this.dataService.order.items.length == 0) {
                    this.router.navigate(["/"]);
                } else {
                    this.router.navigate(["checkout"]);
                }
            }, err => this.errorMessage = "Failed to login");
    }
}
