using LibrarySPA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySPA.Core
{
    public interface IRepository
    {
        IEnumerable<Book> Books { get; }
        IEnumerable<Magazine> Magazines { get; }
        IEnumerable<Newspaper> Newspapers { get; }
        IEnumerable<Genre> Genres { get; }

        void CreateBook(Book entity, int genreId);
        void DeleteBook(int id);
        void DeleteMagazine(int id);
        void DeleteNewspaper(int id);

        Book GetBook(int id);
        Magazine GetMagazine(int id);
        Newspaper GetNewsp(int id);
        Genre GetGenre(int id);

        void UpdateBook(Book book);
    }
}
