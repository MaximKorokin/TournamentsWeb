import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GlobalUserManager, User } from '../global-user-manager';
import { Tournament, TournamentType, TournamentDiscipline } from '../tournaments/tournaments.component';

@Component({
  selector: 'app-create-tournament',
  templateUrl: './create-tournament.component.html',
  styleUrls: ['./create-tournament.component.css']
})
export class CreateTournamentComponent implements OnInit {
  tournament: Tournament;
  type: string;
  discipline: string;

  baseUrl: string;
  types: TournamentType[];
  disciplines: TournamentDiscipline[];

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private manager: GlobalUserManager) {
    this.baseUrl = baseUrl;
    this.tournament = <Tournament>{};
  }

  ngOnInit() {
    this.manager.get("TournamentTypes/", (list) => {
      this.types = <TournamentType[]>list;
    });
    this.manager.get("Disciplines/", (list) => {
      this.disciplines = <TournamentDiscipline[]>list;
    });
  }

  create() {
    let tournament = {
      name: this.tournament.name,
      tournamentTypeId: this.types.find(t => t.name == this.type).id,
      disciplineId: this.disciplines.find(t => t.name == this.discipline).id,
      organizer: this.manager.getUser().name,
      description: this.tournament.description,
      date: this.tournament.date
    };
    this.manager.post("Tournaments/", tournament, null);
  }

}
