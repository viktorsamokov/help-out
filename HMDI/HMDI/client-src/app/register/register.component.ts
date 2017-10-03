import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from "angular-bootstrap-md/modals";
import { Register } from "./register.model";
import { RegisterService } from "./register.service";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
  styles:[`
    .modal-content {width: 400px !important ;}
    .modal-dialog {margin-top: 8% !important ;}    
    `]
})  
export class RegisterComponent implements OnInit {
  @ViewChild('registerForm') public registerForm: ModalDirective;
  
  public registerVm: Register;
  public loading = false;

  constructor(private registerService: RegisterService) {
    this.registerVm = new Register();
   }

  ngOnInit() {
  }

  register(form){
    this.loading = true;
    this.registerService.registerUser(this.registerVm).subscribe(event => {
      this.loading = false;
    });
  }

  open(){
    this.registerForm.show();
  }

}
