import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'angular-bootstrap-md/modals';
import { LoginComponent } from "../login/login.component";
import { RegisterComponent } from "../register/register.component";
import { AuthenticationService } from "../shared/authentication.service";
import { CurrentUser } from "../shared/current-user.model";
import { Observable } from "rxjs/Observable";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  @ViewChild('loginModal') public loginModal: LoginComponent;
  @ViewChild('registerModal') public registerModal: RegisterComponent;

  currentUser: CurrentUser;

  constructor(private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.authenticationService.currentUser.subscribe(user => {
      this.currentUser = user;
    });
  }

  logout(){
    this.authenticationService.logout();
  }

  openLoginModal(){
    this.loginModal.open();
  }

  openRegisterModal(){
    this.registerModal.open();
  }
}
