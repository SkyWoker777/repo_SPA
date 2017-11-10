import { Component, Input } from '@angular/core';
import { Book } from '../../models/book';
import { LibraryService } from '../../shared/library.service';

@Component({
    selector: 'book-detail',
    templateUrl: './book-detail.component.html'
})

export class BookDetailComponent {
    @Input() book: Book;
}