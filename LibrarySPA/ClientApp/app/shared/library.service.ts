import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Response, Headers } from '@angular/http';
import { Book } from '../models/book';
import { Genre } from '../models/genre';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class LibraryService {
    //private baseUrl = "http://localhost:57878";
    //private genreName = Book.prototype.Genre.Name;

    constructor(
        private http: Http,
        @Inject('BASE_URL') private baseUrl: string
    ) { }

    getBooks() {
        return this.http.get(`${this.baseUrl}/api/books`);
    }

    getBooksByGenre(genreId: number) {
        return this.http.get(`${this.baseUrl}/api/books/bygenre/` + genreId);
    }

    getGenre(genreId: number) {
        return this.http.get(`${this.baseUrl}/api/genres/` + genreId);
    }

    getBook(id: number) {
        return this.http.get(`${this.baseUrl}/api/books/` + id);
    }

    createBook(genreId: number, obj: Book) {
        const body = JSON.stringify(obj);
        let headers = new Headers({ 'Content-Type': 'application/json;charset=utf-8' });
        return this.http.post(`${this.baseUrl}/api/books/` + genreId, body, { headers: headers }); 
    }

    deleteBook(id: number) {
        return this.http.delete(`${this.baseUrl}/api/books/` + id);
    }

    updateBook(id: number, obj: Book) {
        const body = JSON.stringify(obj);
        let headers = new Headers({ 'Content-type': 'application/json;charset=utf-8' });
        return this.http.put(`${this.baseUrl}/api/books/` + id, body, { headers: headers });
    }
}