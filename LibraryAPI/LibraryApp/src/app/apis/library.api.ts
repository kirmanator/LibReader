import { inject, Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { Book } from "../models/book.model";
import { environment } from "../../environments/environment.development";

@Injectable({ providedIn: 'root' })
export class LibraryApi {
    private readonly baseUrl: string = environment.baseUrl;
    private readonly endpointName: string = '/Book'
    private readonly url: string = this.baseUrl + this.endpointName;

    private readonly http: HttpClient = inject(HttpClient);

    public getAll(): Observable<Book[]> {
        return this.http.get<Book[]>(`${this.url}`);
    }

    public getFeatured(): Observable<Book[]> {
        return this.http.get<Book[]>(`${this.url}/featured`);
    }

    public getPaginated(pageSize: number, pageNumber: number): Observable<Book[]> {
        const params = new HttpParams().append('pageSize', pageSize).append('pageNumber', pageNumber);
        return this.http.get<Book[]>(`${this.url}/browse`, { params });
    }

    public getAllByIds(bookIds: number[]): Observable<Book[]> {
        return this.http.post<Book[]>(`${this.url}/batch`, bookIds);
    }

    public getById(id: number): Observable<Book> {
        return this.http.get<Book>(`${this.url}/${id}`);
    }

    public isAvailable(id: number): Observable<boolean> {
        return this.http.get<boolean>(`${this.url}/${id}/available`);
    }

    public getTotalBooks(): Observable<number> {
        return this.http.get<number>(`${this.url}/total`);
    }

    public searchForSimilarTitles(query: string): Observable<Book[]> {
        return this.http.post<Book[]>(`${this.url}/search`, JSON.stringify(query.toLowerCase()), {headers: new HttpHeaders({
            'Content-Type': 'application/json'
        })});
    }

    // public advancedSearchForBooks(query: string): Observable<string> {
    //     const searchUrl = 'https://api.bigbookapi.com/search-books?query=' + query.replace(' ', '+') + 'api-key=' + environment.bigBookKey;
    //     return this.http.get<string>(`${searchUrl}`);
    // }

    public insert(book: Book): Observable<void> {
        return this.http.post<void>(`${this.url}`, book);
    }

    public updateById(book: Book): Observable<void> {
        return this.http.put<void>(`${this.url}/${book.bookId}`, book);
    }

    public setAvailable(id: number, available: boolean): Observable<void> {
        return this.http.put<void>(`${this.url}/${id}/available`, available);
    }

    public deleteById(id: number): Observable<Book> {
        return this.http.delete<Book>(`${this.url}/${id}`);
    }
}
