import { Component, OnInit } from '@angular/core';
import { DataService } from '../shared/dataService';

@Component({
  selector: 'app-loginpartial',
  templateUrl: 'loginpartial.component.html',
  styles: []
})
export class LoginpartialComponent implements OnInit {

  constructor(private dataService: DataService) { }

  ngOnInit() {
  }

  //logout() {
  //    this.dataService.logoff();
  //}
}
