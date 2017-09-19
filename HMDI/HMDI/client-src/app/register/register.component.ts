import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from "angular-bootstrap-md/modals";
import { Register } from "./register.model";
import { RegisterService } from "./register.service";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
  styles:['.modal-content {width: 400px !important}']
})  
export class RegisterComponent implements OnInit {
  @ViewChild('registerForm') public registerForm: ModalDirective;
  
  public registerVm: Register;
  
  constructor(private registerService: RegisterService) {
    this.registerVm = new Register();
   }

  ngOnInit() {
  }

  register(form){
    console.log(this.registerVm);
    this.registerService.registerUser(this.registerVm).subscribe(event => {
      console.log("success", event);
    });
  }

  open(){
    this.registerForm.show();
  }

}
