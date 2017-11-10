import { Component, OnInit, trigger, state, style, transition, animate, Inject } from '@angular/core';
import { TemplateRef, ViewChild } from '@angular/core';
import { Book } from '../../models/Book';
import { LibraryService } from '../../shared/library.service';
import { Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/Rx';

@Component({
        selector: 'books',
        templateUrl: './books.component.html',
        providers: [LibraryService]
}) 


export class BooksComponent implements OnInit {
    @ViewChild('readOnlyTemplate') readOnlyTemplate: TemplateRef<any>;
    @ViewChild('editTemplate') editTemplate: TemplateRef<any>;

    editedBook: Book | any;
    books: Array<Book>;
    isNewRecord: boolean;
    statusMessage: string;

    constructor(private serv: LibraryService) {
        this.books = new Array<Book>();
    }

    ngOnInit() {
        this.loadBooks();
    }

    //загрузка books
    private loadBooks() {
        this.serv.getBooks().subscribe((resp: Response) => {
            this.books = resp.json() as Book[];
        });
        console.log("books initialized");
    }

    //todo: need "create template"
    addBook() {
        this.editedBook = new Book(0, "", "", 0, "", 0, 0, "", 0, { id: 0, name: "" });
        //this.books.push(this.editedBook);
        this.isNewRecord = true;
    }

    onSelect(book: Book): void {
        this.editedBook = new Book(
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

    // edit or readonly
    loadTemplate(book: Book) {
        if (this.editedBook && this.editedBook.id == book.id) {
            return this.editTemplate;
        } else {
            return this.readOnlyTemplate;
        }
    }

    saveBook() {
        if (this.isNewRecord) {
            this.serv.createBook(this.editedBook.gener_id, this.editedBook).subscribe((resp: Response) => {
                if (resp.ok) {
                    this.statusMessage = 'Successfully added data.';
                    this.loadBooks();
                }
            }, error => { console.log(`There was an issue. ${error._body}.`) });
            this.isNewRecord = false;
            this.editedBook = null;
        } else {
            this.serv.updateBook(this.editedBook.id, this.editedBook).subscribe((resp: Response) => {
                if (resp.ok) {
                    this.statusMessage = 'The data was successfully updated.';
                    this.loadBooks();
                }
            }, error => { console.log(`There was an issue. ${error._body}.`) });
            this.editedBook = null;
        }
    }

    cancel() {
        if (this.isNewRecord) {
            this.books.pop();
            this.isNewRecord = false;
        }
        this.editedBook = null;
        this.loadBooks();
    }

    deleteBook(book: Book) {
        this.serv.deleteBook(book.id).subscribe((resp: Response) => {
            this.statusMessage = `${book.name} successfully deleted!`;
            this.loadBooks();
        });
    }
}