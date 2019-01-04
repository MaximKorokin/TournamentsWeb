import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GlobalUserManager, User } from '../global-user-manager';
import { Tournament, UsersTournament } from '../tournaments/tournaments.component';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {
  user: User;
  oldPassword: string;
  newPassword1: string;
  newPassword2: string;

  constructor(private route: ActivatedRoute, private manager: GlobalUserManager) { }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get('id');
    this.user = this.manager.getUser();
    if (this.user.name !== undefined && this.user.id.toString() !== id) {
      this.user = undefined;
      return;
    }
  }

  edit() {
    if (this.manager.hash(this.oldPassword) === this.user.password && this.newPassword1 === this.newPassword2) {
      this.user.password = this.newPassword1;
    }
    this.manager.put("Users/" + this.user.id, this.user);
    this.manager.setUser(this.user);
  }

}
