import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GlobalUserManager, User } from '../global-user-manager';
import { Tournament, UsersTournament } from '../tournaments/tournaments.component';
import { TournamentGame } from '../tournament-game/tournament-game.component';

@Component({
  selector: 'app-manage-tournament',
  templateUrl: './manage-tournament.component.html',
  styleUrls: ['./manage-tournament.component.css']
})
export class ManageTournamentComponent implements OnInit {

  tournament: Tournament;
  usersTournaments: UsersTournament[];
  tournamentGames: TournamentGame[];
  organizer: User;

  gamesCounts: Array<number> = new Array<number>();

  emptyUser: string = "empty";

  constructor(private route: ActivatedRoute, private manager: GlobalUserManager) { }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get('id');

    this.manager.get("Tournaments/" + id, (tournament) => {
      this.tournament = tournament;
      
      let curLength = this.tournament.membersCapacity;
      for (let i = 0; i < Math.log2(this.tournament.membersCapacity); i++) {
        curLength /= 2;
        this.gamesCounts.push(curLength);
      }
      console.log(this.gamesCounts);

    });

    this.manager.get("UsersTournaments/" + id, (usersTournaments) => {
      this.usersTournaments = usersTournaments;

      this.manager.get("TournamentsGames/t/" + id, (tournamentGames) => {
        this.tournamentGames = tournamentGames;

        for (let i = 0; i < this.tournamentGames.length; i++) {
          this.manager.get("TournamentsGames/u/" + this.tournamentGames[i].id, users => {
            for (let i = 0; i < users.length; i++) {
              let u = this.usersTournaments.find(ut => ut.userId === users[i].userId);
              users[i] = u !== undefined ? u.user : u;
            }
            this.tournamentGames[i].users = users;
          });
        }

      });

    });
  }

  startTournament() {
    if (!confirm("Are you sure?"))
      return;
    this.manager.put("Tournaments/s/" + this.tournament.id, {});
  }
  deleteTournament() {
    if (!confirm("Are you sure?"))
      return;
    this.manager.delete("Tournaments/" + this.tournament.id);
  }

  removeUser(id: number) {
    if (!confirm("Are you sure?"))
      return;
    this.manager.delete("UsersTournaments/" + id + "/" + this.tournament.id);
    document.getElementById("user" + id).remove();
  }
  addUser() {
    let userName = (<HTMLInputElement>document.getElementById("userName")).value;
    this.manager.get("Users?name=" + userName, (user) => {
      this.manager.post("UsersTournaments/", {
        userId: user.id,
        tournamentId: this.tournament.id,
      }, null);
    });
  }

  generate() {
    this.clearTournamentGames();
    
    if (this.tournament.tournamentType.name === "Single Elimination")
      setTimeout(this.generateSingleElimination.bind(this), 200);
  }

  clearTournamentGames() {
    if (this.tournamentGames === undefined)
      return;
    for (let i = 0; i < this.tournamentGames.length; i++) {
      this.manager.delete("TournamentsGames/u/" + this.tournamentGames[i].id);
    }
  }

  generateSingleElimination() {
    if (this.usersTournaments === undefined || this.usersTournaments.length < 2 || Math.log2(this.usersTournaments.length) % 1 !== 0)
      return;
    this.manager.get("TournamentsGames", (gameId) => {

      for (let i = 0; i < this.usersTournaments.length; i++) {
        this.createTournamentGamePlayer(gameId, this.usersTournaments[i].userId);

        if (i % 2 === 1) {
          this.sleep(200);
          this.createTournamentGame(gameId++, Math.floor(i / 2 + 1), this.usersTournaments[i].userId);
        }
      }
      for (let i = 0; i < this.usersTournaments.length - 2; i++) {
        this.createTournamentGamePlayer(gameId, i % 2 === 0 ? 10 : 11);

        if (i % 2 === 1) {
          this.sleep(200);
          this.createTournamentGame(gameId++, Math.floor((i + this.usersTournaments.length) / 2 + 1), 10);
        }
      }
      this.sleep(100);
      this.ngOnInit();
    });
  }

  createTournamentGamePlayer(gameId: number, userId: number) {
    this.manager.post("TournamentsGames/u/", {
      tournamentsGameId: gameId,
      userId: userId,
    }, null);
  }

  createTournamentGame(id: number, num: number, winnerId: number) {
    this.manager.post("TournamentsGames", {
      id: id,
      tournamentId: this.tournament.id,
      winnerId: winnerId,
      number: num,
      isCompleted: false,
    }, null);
  }
  
  setGameWinner(game: TournamentGame, userId: number) {

    this.manager.put("TournamentsGames/", {
      id: game.id,
      tournamentId: this.tournament.id,
      winnerId: userId,
      number: game.number,
      isCompleted: true,
    });

    if (game.number === this.tournament.membersCapacity - 1) {
      this.sleep(200);
      this.ngOnInit();
      return;
    }

    let nextNumber = this.calculateNextGameNumber(game.number, this.tournament.membersCapacity);
    let nextGame = this.tournamentGames.find(tg => tg.number === nextNumber);

    this.manager.get("TournamentsGames/u/" + nextGame.id, users => {

      let fakeId1 = users.find(u => u.userId !== nextGame.winnerId).userId;
      let fakeId2 = users.find(u => u.userId === nextGame.winnerId).userId;

      nextGame.winnerId = fakeId1;

      this.manager.put("TournamentsGames/", nextGame);
      this.sleep(200);
      this.manager.put("TournamentsGames/u/" + fakeId2, {
        tournamentsGameId: nextGame.id,
        userId: userId,
      });

    });
    this.sleep(200);
    this.ngOnInit();
  }

  calculateNextGameNumber(cur, all) { // math magic
    cur = cur % 2 === 1 ? cur + 1 : cur;
    return (all + cur) / 2;
  }

  sleep(milliseconds: number) {
    let e = new Date().getTime() + milliseconds;
    while (new Date().getTime() <= e) { }
  }

  sumPrevGamesCount(index: number) {
    let sum = 0;
    for (let i = 0; i < index; i++) {
      sum += this.gamesCounts[i];
    }
    return sum;
  }

}

export interface TournamentGamePlayer {
  gameId: number,
  userId: number,
  user: User,
}

export interface TournamentGame {
  id: number,
  tournamentId: number,
  winnerId: number,
  number: number,
  isCompleted: boolean,

  users: User[],
}
