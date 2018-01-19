import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { ToastsManager } from 'ng2-toastr/ng2-toastr';

@Component({
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent implements OnInit {
  @ViewChild('registerModal') public registerModal: RegisterComponent;
  
  constructor() {
   }

  ngOnInit() {
  }

  openRegisterModal(){
    this.registerModal.open();
  }

}
