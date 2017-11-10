using Dapper;
using LibrarySPA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using LibrarySPA.Core.Helpers;

namespace LibrarySPA.Core
{
    public class LibraryRepository : IRepository
    {
        private string _connectionString = null;

        public LibraryRepository(string connection)
        {
            _connectionString = connection;
        }

        public IEnumerable<Book> Books
        {
            get
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var sql = @"SELECT * FROM Books
                                SELECT * FROM Genres WHERE ID IN (SELECT Genre_ID FROM Books)";

                    var mapped = db.QueryMultiple(sql)
                        .Map<Book, Genre, int>(
                        book => book.Genre_ID,
                        genre => genre.ID,
                        (book, genre) => { book.Genre = genre;});
                    return mapped;
                }
            }
        }
        public IEnumerable<Magazine> Magazines
        {
            get
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    return db.Query<Magazine>("SELECT * FROM Magazines").ToList();
                }
            }
        }
        public IEnumerable<Newspaper> Newspapers
        {
            get
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    return db.Query<Newspaper>("SELECT * FROM Newspapers").ToList();
                }
            }
        }
        public IEnumerable<Genre> Genres
        {
            get
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    return db.Query<Genre>("SELECT * FROM Genres").ToList();
                }
            }
        }

        public void CreateBook(Book entity, int genreId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                //entity.Genre = GetGenre(genreId);
                entity.Genre_ID = genreId;
                string sqlQuery = "INSERT INTO Books " +
                    "VALUES(@Name, @Author, @Year, @PubHouse, @PageCount, @Cost, @Descriprion, @Genre_ID)";
                db.Execute(sqlQuery, entity);
            }
        }
        public void DeleteBook(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"DELETE FROM Books WHERE ID = {id}";
                db.Execute(sqlQuery);
            }
        }
        public void DeleteMagazine(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"DELETE FROM Magazines WHERE ID = {id}";
                db.Execute(sqlQuery);
            }
        }
        public void DeleteNewspaper(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"DELETE FROM Newspapers WHERE ID = {id}";
                db.Execute(sqlQuery);
            }
        }

        public Book GetBook(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = $@"SELECT * FROM Books WHERE ID = {id}
                             SELECT * FROM Genres WHERE ID IN (SELECT Genre_ID FROM Books WHERE ID = {id})";

                var mapped = db.QueryMultiple(sql)
                    .MapForSingle<Book, Genre, int>(
                    book => book.Genre_ID,
                    genre => genre.ID,
                    (book, genre) => { book.Genre = genre; });
                return mapped;
            }
        }
        public Magazine GetMagazine(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Magazine>($"SELECT * FROM Magazines WHERE ID = {id}").FirstOrDefault();
            }
        }
        public Newspaper GetNewsp(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Newspaper>($"SELECT * FROM Newspapers WHERE ID = {id}").FirstOrDefault();
            }
        }

        public Genre GetGenre(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Genre>($"SELECT * FROM Genres WHERE ID = {id}").FirstOrDefault();
            }
        }

        public void UpdateBook(Book book)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "UPDATE Books " +
                    "SET Name = @Name," +
                        "Author = @Author, " +
                        "Year = @Year," +
                        "PubHouse = @PubHouse, " +
                        "PageCount = @PageCount," +
                        "Cost = @Cost," +
                        "Description = @Description" +
                    " WHERE ID = @ID";
                db.Execute(sqlQuery, book);
            }
        }
    }
}
