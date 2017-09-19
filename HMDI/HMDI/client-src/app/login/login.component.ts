import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from "angular-bootstrap-md";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  styles:['.modal-content {width: 400px !important}']
})
export class LoginComponent implements OnInit {
  @ViewChild('loginForm') public loginForm: ModalDirective;
  
  constructor() { }

  ngOnInit() {
  }

  open(){
    this.loginForm.show();
  }
}
