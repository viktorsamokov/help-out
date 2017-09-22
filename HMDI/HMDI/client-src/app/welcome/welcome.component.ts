import { Component, OnInit, ViewChild } from '@angular/core';
import { RegisterComponent } from "../register/register.component";

@Component({
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent implements OnInit {
  @ViewChild('registerModal') public registerModal: RegisterComponent;
  
  constructor() { }

  ngOnInit() {
  }

  openRegisterModal(){
    this.registerModal.open();
  }

}
