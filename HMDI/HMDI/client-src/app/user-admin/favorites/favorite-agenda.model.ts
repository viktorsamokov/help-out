import { Agenda } from "../agendas/category-agendas/category-agenda.model";


export class FavoriteAgenda{
    AgendaId: number;
    Agenda: Agenda;
    HasRated: boolean;
    Grade: number;
    UserId: string;
    state: string;  
}