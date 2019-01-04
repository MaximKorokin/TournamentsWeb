import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GlobalUserManager, User } from '../global-user-manager';
import { Tournament, UsersTournament } from '../tournaments/tournaments.component';

@Component({
  selector: 'app-tournament',
  templateUrl: './tournament.component.html',
  styleUrls: ['./tournament.component.css']
})
export class TournamentComponent implements OnInit {
  tournament: Tournament;
  usersTournaments: UsersTournament[];
  organizer: User;

  constructor(private route: ActivatedRoute, private manager: GlobalUserManager) { }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get('id');

    this.manager.get("Tournaments/" + id, (tournament) => {
      this.tournament = tournament;
      console.log(tournament);
      if (tournament.organizer !== null)
        this.getOrganizer(tournament.organizer);
    });

    this.manager.get("UsersTournaments/" + id, (usersTournaments) => {
      this.usersTournaments = usersTournaments;
      console.log(usersTournaments);
    });
  }

  getOrganizer(name: string) {
    console.log(name);
    this.manager.get("Users?name=" + name, (user) => {
      this.organizer = user;
    });
  }

  hasUserCollision() {
    let user = this.manager.getUser();
    let hasCol = this.usersTournaments.find(ut => ut.userId == user.id) == undefined;
    return hasCol;
  }

  joinTournament() {
    this.manager.post("UsersTournaments/", {
      userId: this.manager.getUser().id,
      tournamentId: this.tournament.id,
    }, null);
  }
  leaveTournament() {
    this.manager.delete("UsersTournaments/" + this.manager.getUser().id + "/" + this.tournament.id);
  }

}
