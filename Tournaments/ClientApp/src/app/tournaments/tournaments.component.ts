import { Component, OnInit, Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PlatformLocation } from '@angular/common';
import { GlobalUserManager, User } from '../global-user-manager';
import { Router } from "@angular/router";

@Component({
  selector: 'app-tournaments',
  templateUrl: './tournaments.component.html',
  styleUrls: ['./tournaments.component.css']
})
export class TournamentsComponent implements OnInit {

  tournaments: Tournament[];

  searchString: string = "";

  constructor(private manager: GlobalUserManager, private router: Router) { }

  ngOnInit() {
    this.manager.get("Tournaments/", list => {
      this.tournaments = <Tournament[]>list;
    });
  }

  search() {
    this.manager.get("Tournaments/" + (this.searchString === "" ? "" : "s/") + this.searchString, list => {
      this.tournaments = <Tournament[]>list;
    });
  }

}

export interface Tournament {
  id: number;
  name: string;
  description: string;
  date: Date;
  tournamentTypeId: number;
  tournamentType: TournamentType;
  disciplineId: number;
  discipline: TournamentDiscipline;
  membersCapacity: number;
  isCompleted: boolean;
  isStarted: boolean;
}

export interface TournamentType {
  id: number;
  name: string;
}

export interface TournamentDiscipline {
  id: number;
  name: string;
}

export interface UsersTournament {
  userId: number;
  tournamentId: number;
  user: User;
  tournament: Tournament;
}
