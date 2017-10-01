import './rxjs-extensions';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { HttpModule } from '@angular/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthGuardService } from './auth-guard.service';
import { WelcomeComponent } from './welcome/welcome.component';
import { MainComponent } from './main/main.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { FormsModule } from "@angular/forms";
import { RegisterService } from './register/register.service';
import { AuthenticationService } from './shared/authentication.service';
import { UserAdminComponent } from './user-admin/user-admin.component';
import { UserPublicComponent } from './user-public/user-public.component';
import { AgendasComponent } from './user-admin/agendas/agendas.component';
import { FavoritesComponent } from './user-admin/favorites/favorites.component';
import { PlannerComponent } from './user-admin/planner/planner.component';
import { AgendasService } from './user-admin/agendas/agendas.service';
import { PlannerService } from './user-admin/planner/planner.service';
import { FavoritesService } from './user-admin/favorites/favorites.service';
import { CategoryAgendasComponent } from './user-admin/agendas/category-agendas/category-agendas.component';
import { AgendaModalComponent } from './user-admin/agenda-modal/agenda-modal.component';
import { ModalsService } from './user-admin/modals.service';

@NgModule({
  declarations: [
    AppComponent,
    WelcomeComponent,
    MainComponent,
    NavbarComponent,
    FooterComponent,
    LoginComponent,
    RegisterComponent,
    UserAdminComponent,
    UserPublicComponent,
    AgendasComponent,
    FavoritesComponent,
    PlannerComponent,
    CategoryAgendasComponent,
    AgendaModalComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    HttpModule,
    MDBBootstrapModule.forRoot()
  ],
  bootstrap: [AppComponent],
  schemas: [ NO_ERRORS_SCHEMA ],
  providers: [
    AuthGuardService, 
    RegisterService, 
    AuthenticationService, 
    AgendasService, 
    PlannerService,
    FavoritesService,
    ModalsService
    ]
})
export class AppModule { }
