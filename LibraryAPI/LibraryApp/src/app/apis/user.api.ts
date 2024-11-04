import { inject, Injectable } from "@angular/core";
import { HttpClient, HttpParams, HttpResponse } from "@angular/common/http";
import { Observable } from "rxjs";
import { UserInfo } from "../models/user-info.model";
import { CredentialsDto } from "../dtos/credentials-dto.model";
import { environment } from "../../environments/environment.development";

@Injectable({ providedIn: 'root' })
export class UserApi {
    private readonly baseUrl: string = environment.baseUrl;
    private readonly endpointName: string = '/UserInfo'
    private readonly url: string = this.baseUrl + this.endpointName;

    private readonly http: HttpClient = inject(HttpClient);

    public getAll(): Observable<UserInfo[]> {
        return this.http.get<UserInfo[]>(`${this.url}`);
    }

    public getById(id: number): Observable<UserInfo> {
        return this.http.get<UserInfo>(`${this.url}/${id}`);
    }

    public getByUsername(username: string): Observable<UserInfo> {
        const params = new HttpParams().append('username', username);
        return this.http.get<UserInfo>(`${this.url}/username`, { params });
    }

    public existsByUsername(username: string): Observable<boolean> {
        const params = new HttpParams().append('username', username);
        return this.http.get<boolean>(`${this.url}/username/exists`, {params });
    }

    public loginWithCredentials(credentials: CredentialsDto): Observable<UserInfo> {
        return this.http.post<UserInfo>(`${this.url}/login`, credentials);
    }

    public createAccount(userInfo: UserInfo): Observable<void> {
        return this.http.post<void>(`${this.url}`, userInfo);
    }

    public updateById(userInfo: UserInfo): Observable<void> {
        return this.http.put<void>(`${this.url}/${userInfo.userId}`, userInfo);
    }

    public deleteById(id: number): Observable<UserInfo> {
        return this.http.delete<UserInfo>(`${this.url}/${id}`);
    }
}
