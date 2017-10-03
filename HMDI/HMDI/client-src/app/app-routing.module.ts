import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from "./main/main.component";
import { WelcomeComponent } from "./welcome/welcome.component";
import { AuthGuardService } from "./auth-guard.service";
import { UserAdminComponent } from "./user-admin/user-admin.component";
import { AgendasComponent } from "./user-admin/agendas/agendas.component";
import { PlannerComponent } from "./user-admin/planner/planner.component";
import { FavoritesComponent } from "./user-admin/favorites/favorites.component";
import { CategoryAgendasComponent } from "./user-admin/agendas/category-agendas/category-agendas.component";
import { DailyComponent } from './user-admin/planner/daily/daily.component';
import { WeeklyComponent } from './user-admin/planner/weekly/weekly.component';
import { ActiveComponent } from './user-admin/planner/active/active.component';

const routes: Routes = [
  { path: 'main', component: MainComponent, canActivate: [AuthGuardService] },
  { path: 'admin', component: UserAdminComponent, canActivate: [AuthGuardService],
    children: [
      { path: '', redirectTo: 'agendas', pathMatch: 'full', canActivate: [AuthGuardService] },
      { path: 'agendas', component: AgendasComponent, canActivate: [AuthGuardService],
        children: [
          {path: 'category/:id', component: CategoryAgendasComponent, canActivate: [AuthGuardService]}
        ]
      },
      { path: 'planner', component: PlannerComponent, canActivate: [AuthGuardService], 
        children: [
          { path: 'today', component: DailyComponent, canActivate: [AuthGuardService]},
          { path: 'week', component: WeeklyComponent, canActivate: [AuthGuardService]},
          { path: 'active', component: ActiveComponent, canActivate: [AuthGuardService]},
          { path: '', redirectTo: 'today', pathMatch: 'full'}
        ]
      },
      { path: 'favorites', component: FavoritesComponent, canActivate: [AuthGuardService] }
    ] 
  },
  { path: 'welcome', component: WelcomeComponent},
  { path: '', redirectTo: 'main', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
