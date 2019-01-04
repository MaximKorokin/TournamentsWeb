import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageTournamentComponent } from './manage-tournament.component';

describe('ManageTournamentComponent', () => {
  let component: ManageTournamentComponent;
  let fixture: ComponentFixture<ManageTournamentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageTournamentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageTournamentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
