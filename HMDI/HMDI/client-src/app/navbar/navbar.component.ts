import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'angular-bootstrap-md/modals';
import { LoginComponent } from "../login/login.component";
import { RegisterComponent } from "../register/register.component";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  @ViewChild('loginModal') public loginModal: LoginComponent;
  @ViewChild('registerModal') public registerModal: RegisterComponent;
  
  constructor() { }

  ngOnInit() {
  }

  openLoginModal(){
    this.loginModal.open();
  }

  openRegisterModal(){
    this.registerModal.open();
  }

}
