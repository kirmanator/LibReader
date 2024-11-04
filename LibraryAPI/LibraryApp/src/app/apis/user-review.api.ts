import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment.development";
import { UserReview } from "../models/user-review.model";
import { HttpClient, HttpParams } from "@angular/common/http";
import { UserReviewDto } from "../dtos/user-review-dto.model";

@Injectable({ providedIn: 'root' })
export class UserReviewApi {
    private readonly baseUrl: string = environment.baseUrl;
    private readonly endpointName: string = '/UserReview'
    private readonly url: string = this.baseUrl + this.endpointName;

    private readonly http: HttpClient = inject(HttpClient);

    public getAll(): Observable<UserReview[]> {
        return this.http.get<UserReview[]>(`${this.url}`);
    }

    public getById(id: number): Observable<UserReview> {
        return this.http.get<UserReview>(`${this.url}/${id}`);
    }
    
    public getAllByUser(userId: number): Observable<UserReview[]> {
        const params = new HttpParams().append('userId', userId);
        return this.http.get<UserReview[]>(`${this.url}/user`, { params });
    }

    public getAllByBook(bookId: number): Observable<UserReviewDto[]> {
        const params = new HttpParams().append('bookId', bookId);
        return this.http.get<UserReviewDto[]>(`${this.url}/book`, { params });
    }

    public insert(userReview: UserReview): Observable<void> {
        return this.http.post<void>(`${this.url}`, userReview);
    }

    public updateById(userReview: UserReview): Observable<void> {
        return this.http.put<void>(`${this.url}/${userReview.userReviewId}`, userReview);
    }

    public deleteById(id: number): Observable<UserReview> {
        return this.http.delete<UserReview>(`${this.url}/${id}`);
    }
}
