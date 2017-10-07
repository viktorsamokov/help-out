import { Component, OnInit } from '@angular/core';
import { FavoriteAgenda } from './favorite-agenda.model';
import { FavoritesService } from './favorites.service';
import { trigger, state, style, animate, transition } from '@angular/animations';

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
  constructor(private favoriteService: FavoritesService) { }

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

  }

  rate(favoriteAgenda){
    console.log(favoriteAgenda);
    this.favoriteService.rateAgenda(favoriteAgenda).subscribe(val => {
      favoriteAgenda.HasRated = true;
    });
  }
}
