import { AgendaItem } from "./agenda-item.model";

export class Agenda {
    Id: number;
    Title: string;
    Description: string;
    IsDeleted: boolean;
    Status: number;
    DateCreated: Date;
    AgendaCategoryId: number;
    Items: Array<AgendaItem>;
}