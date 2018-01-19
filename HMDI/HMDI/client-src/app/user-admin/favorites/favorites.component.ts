import { Component, OnInit } from '@angular/core';
import { FavoriteAgenda } from './favorite-agenda.model';
import { FavoritesService } from './favorites.service';
import { trigger, state, style, animate, transition } from '@angular/animations';
import { Checklist } from '../planner/checklist.model';
import { ChecklistItem } from '../planner/checklist-item.model';
import { ModalsService } from '../modals.service';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.scss'],
  animations: [
    trigger('animateHeight', [
        state('inactive', style({
          height: '0',
          opacity: 0
        })),
        state('active', style({
          height: '*',
          opacity: 1
        })),
        transition('inactive => active', animate('300ms ease-in')),
        transition('active => inactive', animate('300ms ease-out'))
    ])
  ] 
})
export class FavoritesComponent implements OnInit {
  favorites: FavoriteAgenda[] = [];
  grade: number = 1;
  loading = false;
  constructor(private modalService: ModalsService, private favoriteService: FavoritesService) { }

  ngOnInit() {
    this.favoriteService.getFavorites().subscribe(val => {
      this.favorites = val;
    })
  }

  toggleAgenda(agenda){
    if(!agenda.state){
      agenda.state = "active";
    }else {
      agenda.state = (agenda.state === 'inactive' ? 'active' : 'inactive');      
    }
  }

  removeFromFavorites(favoriteAgenda){
    this.loading = true;
    this.favoriteService.removeFavorite(favoriteAgenda).subscribe(agenda => {
      this.loading = false;
      console.log(agenda);
      for (var index = 0; index < this.favorites.length; index++) {
        var element = this.favorites[index];
        if(element.AgendaId == favoriteAgenda.AgendaId && element.UserId == favoriteAgenda.UserId){
          this.favorites.splice(index, 1);
        }
      }
      console.log(this.favorites);
    });
  }

  convertToChecklist(favoriteAgenda){
    let checklist = new Checklist();
    checklist.state = "inactive";
    checklist.Title = favoriteAgenda.Agenda.Title;
    checklist.IsFinished = false;
    checklist.Items = new Array<ChecklistItem>();
    favoriteAgenda.Agenda.Items.forEach(element => {
      let checklistItem = new ChecklistItem();
      checklistItem.IsChecked = false;
      checklistItem.Todo = element.Todo;
      checklist.Items.push(checklistItem);
    });

    let data = { task: checklist, planner: '' };
    this.modalService.openChecklistModal(data);
  }

  rate(favoriteAgenda){
    console.log(favoriteAgenda);
    this.loading = true;
    this.favoriteService.rateAgenda(favoriteAgenda).subscribe(val => {
      this.loading = false;
      favoriteAgenda.HasRated = true;
    });
  }
}
