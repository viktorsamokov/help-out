import {Router, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivate } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthenticationService } from "./shared/authentication.service";

@Injectable()
export class AuthGuardService implements CanActivate {

  constructor(private router:Router, private authenticationService: AuthenticationService) {  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean{
    if(!this.authenticationService.isAuthenticated()){
      this.router.navigate(['welcome']);
      return false;      
    }

    return true;
  }
}
