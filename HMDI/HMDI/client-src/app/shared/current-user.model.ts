import { LoggedInUser } from "./logged-in-user.model";

export class CurrentUser {
    token: string;
    expiration: string;
    user: LoggedInUser;
}

