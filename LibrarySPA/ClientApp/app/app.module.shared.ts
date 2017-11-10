import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, PreloadAllModules } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { BooksComponent } from './components/books/books.component';
import { BookDetailComponent } from './components/books/book-detail.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';

//import { ButtonsModule } from '@progress/kendo-angular-buttons';
//import { GridModule } from '@progress/kendo-angular-grid';

import { LibraryService } from './shared/library.service';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        FetchDataComponent,
        BooksComponent,
        BookDetailComponent,
        HomeComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,

        //GridModule,
        //ButtonsModule,

        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            {
                path: 'books',
                component: BooksComponent,
                data: {
                    title: 'Books'
                }
            },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'home' }
        ], {
                useHash: false,
                preloadingStrategy: PreloadAllModules,
                initialNavigation: 'enabled'
            })
    ],
    providers: [
        LibraryService
    ]
})
export class AppModuleShared {
}
