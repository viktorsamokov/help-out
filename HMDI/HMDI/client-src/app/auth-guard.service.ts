import {Router, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivate } from '@angular/router';
import { Injectable } from '@angular/core';

@Injectable()
export class AuthGuardService implements CanActivate {
  canOpen: boolean;

  constructor(private router:Router) { 
    this.canOpen = false;
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean{
    console.log(state);
    console.log(route);
    if(!this.canOpen){
      this.router.navigate(['welcome']);
    }

    return this.canOpen;
  }
}
