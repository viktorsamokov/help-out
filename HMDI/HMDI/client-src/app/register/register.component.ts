import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { ModalDirective } from "angular-bootstrap-md/modals";
import { Register } from "./register.model";
import { RegisterService } from "./register.service";
import { ToastsManager } from 'ng2-toastr';

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

  constructor(private registerService: RegisterService, public toastr: ToastsManager, vcr: ViewContainerRef) {
    this.registerVm = new Register();
    this.toastr.setRootViewContainerRef(vcr);
   }

  ngOnInit() {
  }

  register(form){
    this.loading = true;
    this.registerService.registerUser(this.registerVm).subscribe(event => {
      this.loading = false;
      this.registerForm.hide();
      this.toastr.success("You have succesfully registered, now you can log into Helpout", "Success");      
    });
  }

  open(){
    this.registerForm.show();
  }

}
