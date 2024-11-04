import { inject, Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { BookLoan } from "../models/book-loan.model";
import { environment } from "../../environments/environment.development";

@Injectable({ providedIn: 'root' })
export class LoanApi {
    private readonly baseUrl: string = environment.baseUrl;
    private readonly endpointName: string = '/BookLoan'
    private readonly url: string = this.baseUrl + this.endpointName;

    private readonly http: HttpClient = inject(HttpClient);

    public getAll(): Observable<BookLoan[]> {
        return this.http.get<BookLoan[]>(`${this.url}`);
    }

    public getById(id: number): Observable<BookLoan> {
        return this.http.get<BookLoan>(`${this.url}/${id}`);
    }
    
    public getAllByUser(userId: number): Observable<BookLoan[]> {
        const params = new HttpParams().append('userId', userId);
        return this.http.get<BookLoan[]>(`${this.url}/user`, { params });
    }

    public insert(bookLoan: BookLoan): Observable<void> {
        return this.http.post<void>(`${this.url}`, bookLoan);
    }

    public updateById(bookLoan: BookLoan): Observable<void> {
        return this.http.put<void>(`${this.url}/${bookLoan.loanId}`, bookLoan);
    }

    public deleteById(id: number): Observable<BookLoan> {
        return this.http.delete<BookLoan>(`${this.url}/${id}`);
    }
}
