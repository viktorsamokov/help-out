import { ChecklistItem } from "./checklist-item.model";

export class Checklist {
    Id: number;
    Title: string;
    DateCreated: Date;
    DueDate: Date;
    FinishedAt: Date;
    IsFinished: boolean;
    Items: Array<ChecklistItem>;
    state: string;
}