import { Component, OnInit, trigger, state, style, transition, animate, Inject } from '@angular/core';
import { Book } from '../../models/Book';
import { LibraryService } from '../../shared/library.service';
import { Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/Rx';


@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    providers: [LibraryService]
})
export class HomeComponent implements OnInit {

    books: Array<Book>;
    selectedBook: Book;

    constructor(private service: LibraryService) {
        this.books = new Array<Book>();
    }

    ngOnInit() {
        this.load();
    }

    private load() {
        this.service.getBooks().subscribe((resp: Response) => {
            this.books = resp.json() as Book[];
        });
    }

    onDetails(book: Book) {
        this.selectedBook = new Book(
            book.id,
            book.name,
            book.author,
            book.year,
            book.pubHouse,
            book.pageCount,
            book.cost,
            book.description,
            book.gener_id,
            book.genre
        );
    }
}
