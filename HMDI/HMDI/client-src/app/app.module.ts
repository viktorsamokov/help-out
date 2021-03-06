import './rxjs-extensions';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { HttpModule } from '@angular/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoadingModule, ANIMATION_TYPES } from 'ngx-loading';
import { NgxMyDatePickerModule } from 'ngx-mydatepicker';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {ToastModule} from 'ng2-toastr/ng2-toastr';

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
import { DailyComponent } from './user-admin/planner/daily/daily.component';
import { WeeklyComponent } from './user-admin/planner/weekly/weekly.component';
import { ActiveComponent } from './user-admin/planner/active/active.component';
import { WeeklyService } from './user-admin/planner/weekly/weekly.service';
import { DailyService } from './user-admin/planner/daily/daily.service';
import { ActiveService } from './user-admin/planner/active/active.service';
import { ChecklistModalComponent } from './user-admin/checklist-modal/checklist-modal.component';
import { DayTasksPipe } from './shared/day-tasks.pipe';
import { TagService } from './shared/tag.service';
import { MainService } from './main/main.service';

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
    AgendaModalComponent,
    DailyComponent,
    WeeklyComponent,
    ActiveComponent,
    ChecklistModalComponent,
    DayTasksPipe
  ],
  imports: [
    FormsModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    LoadingModule.forRoot({
      animationType: ANIMATION_TYPES.threeBounce,
      backdropBackgroundColour: 'rgba(0,0,0,0.3)', 
      backdropBorderRadius: '4px',
      primaryColour: '#ffffff', 
      secondaryColour: '#ffffff', 
      tertiaryColour: '#ffffff'
    }),
    NgxMyDatePickerModule.forRoot(),
    NgbModule.forRoot(),
    HttpModule,
    MDBBootstrapModule.forRoot(),
    ToastModule.forRoot()
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
    ModalsService,
    WeeklyService,
    DailyService,
    ActiveService,
    TagService,
    MainService
    ]
})
export class AppModule { }
