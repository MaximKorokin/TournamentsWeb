import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GlobalUserManager, User } from '../global-user-manager';
import { Tournament, UsersTournament } from '../tournaments/tournaments.component';


@Component({
  selector: 'app-user-page',
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.css']
})
export class UserPageComponent implements OnInit {

  user: User;
  organizerTournaments: Tournament[];
  userTournaments: Tournament[];

  constructor(private route: ActivatedRoute, private manager: GlobalUserManager) { }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get('id');
    this.manager.get("Users/" + id, (user) => {
      this.user = user;
      console.log(user);
      this.getTournaments();
    });
  }

  getTournaments() {
    this.manager.get("OrganizerTournaments/" + this.user.name, (list) => {
      this.organizerTournaments = <Tournament[]>list;
    });
    this.manager.get("UserTournaments/" + this.user.id, (list) => {
      console.log(this.user.id);
      this.userTournaments = <Tournament[]>list;
    });
  }

}
