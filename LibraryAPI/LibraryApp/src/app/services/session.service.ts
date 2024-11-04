import { Injectable, signal } from "@angular/core";
import { Session } from "../models/session.model";
import { UserInfo } from "../models/user-info.model";

@Injectable({ providedIn: 'root' })
export class SessionService {
    public static readonly EmptySession = {} as Session;
    private readonly _session = signal<Session>(SessionService.EmptySession);
    readonly session = this._session.asReadonly();
    private readonly isAdmin = signal(false);

    updateSession(user: UserInfo) {
        const credsObj = {user: user};
        this._session.set( credsObj as Session);
    }

    clearSession() {
        this._session.set(SessionService.EmptySession);
    }

    UserId() {
        return this.session().user.userId;
    }

    Username() {
        return this.session().user.username;
    }

    IsAdmin() {
        return this._session().user.roleId != 1;
    }
}

