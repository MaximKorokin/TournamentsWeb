import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './login/login.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { GlobalUserManager } from './global-user-manager';
import { TournamentsComponent } from './tournaments/tournaments.component';
import { CreateTournamentComponent } from './create-tournament/create-tournament.component';
import { TournamentComponent } from './tournament/tournament.component';
import { EditTournamentComponent } from './edit-tournament/edit-tournament.component';
import { UserPageComponent } from './user-page/user-page.component';
import { EditUserComponent } from './edit-user/edit-user.component';
import { ManageTournamentComponent } from './manage-tournament/manage-tournament.component';
import { TournamentGameComponent } from './tournament-game/tournament-game.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    SignUpComponent,
    TournamentsComponent,
    CreateTournamentComponent,
    TournamentComponent,
    EditTournamentComponent,
    UserPageComponent,
    EditUserComponent,
    ManageTournamentComponent,
    TournamentGameComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'sign-up', component: SignUpComponent },
      { path: 'login', component: LoginComponent },
      { path: 'user/:id', component: UserPageComponent },
      { path: 'edit-user/:id', component: EditUserComponent },
      { path: 'tournaments', component: TournamentsComponent },
      { path: 'tournaments/:id', component: TournamentComponent },
      { path: 'create-tournament', component: CreateTournamentComponent },
      { path: 'edit-tournament/:id', component: EditTournamentComponent },
      { path: 'manage-tournament/:id', component: ManageTournamentComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
    ])
  ],
  providers: [GlobalUserManager],
  bootstrap: [AppComponent]
})
export class AppModule { }
