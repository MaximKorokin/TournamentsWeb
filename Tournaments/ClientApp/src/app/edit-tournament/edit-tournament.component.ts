import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GlobalUserManager, User } from '../global-user-manager';
import { Tournament, TournamentType, TournamentDiscipline } from '../tournaments/tournaments.component';

@Component({
  selector: 'app-edit-tournament',
  templateUrl: './edit-tournament.component.html',
  styleUrls: ['./edit-tournament.component.css']
})
export class EditTournamentComponent implements OnInit {

  tournament: Tournament;
  types: TournamentType[];
  disciplines: TournamentDiscipline[];

  constructor(private route: ActivatedRoute, private manager: GlobalUserManager) { }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get('id');

    this.manager.get("Tournaments/" + id, (tournament) => {
      this.tournament = tournament;
      console.log(tournament);
    });
    this.manager.get("TournamentTypes/", (list) => {
      this.types = <TournamentType[]>list;
    });
    this.manager.get("Disciplines/", (list) => {
      this.disciplines = <TournamentDiscipline[]>list;
    });
  }

  edit() {
    this.tournament.tournamentTypeId = this.types.find(t => t.name == this.tournament.tournamentType.name).id;
    this.tournament.disciplineId = this.disciplines.find(d => d.name == this.tournament.discipline.name).id;
    this.manager.put(
      "Tournaments/" + this.tournament.id,
      this.tournament,
    );
  }

  parseDate(dateString: string): Date {
    if (dateString) {
      return new Date(dateString);
    } else {
      return null;
    }
  }

}
