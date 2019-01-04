import { Component, OnInit } from '@angular/core';
import { User } from '../global-user-manager';

@Component({
  selector: 'app-tournament-game',
  templateUrl: './tournament-game.component.html',
  styleUrls: ['./tournament-game.component.css']
})
export class TournamentGameComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}

export interface TournamentGame {
  id: number,
  tournamentId: number,
  winnerId: number,
  number: number,
  isCompleted: boolean,

  users: User[],
}
