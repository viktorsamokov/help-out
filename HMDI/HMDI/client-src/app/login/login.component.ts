import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from "angular-bootstrap-md";
import { Login } from "./login.model";
import { AuthenticationService } from "../shared/authentication.service";
import { Router } from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  styles:['.modal-content {width: 400px !important}']
})
export class LoginComponent implements OnInit {
  @ViewChild('loginForm') public loginForm: ModalDirective;
  loginVm: Login;

  constructor(private authenticationService: AuthenticationService, private router: Router) { 
    this.loginVm = new Login();
  }

  ngOnInit() {
  }

  open(){
    this.loginForm.show();
  }

  login(form){
    this.authenticationService.login(this.loginVm).subscribe(event => {
      this.loginForm.hide();
      this.authenticationService.isAuthenticated();
      this.router.navigate(['main']);
    });
  }
}
