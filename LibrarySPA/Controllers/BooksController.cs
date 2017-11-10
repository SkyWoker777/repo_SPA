using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibrarySPA.Core;
using LibrarySPA.Core.Entities;

namespace LibrarySPA.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IRepository _context;
        public BooksController(IRepository repository)
        {
            _context = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _context.Books.OrderBy(b => b.Name).ToArray();
            if (!books.Any())
            {
                return NotFound("Error. No books found! Try again later.");
            }
            return Ok(books);
        }

        [HttpGet("/books/{id}")]
        public IActionResult Get(int id)
        {
            var book = _context.GetBook(id);

            if (book == null)
            {
                return NotFound("Book not found!");
            }
            return Ok(book);
        }

        [HttpGet("/books/bygenre/{genreId}")]
        public IActionResult ByGenre(int genreId)
        {
            var booksByGenre = _context.Books.Where(x => x.Genre != null && x.Genre.ID == genreId)
                .OrderByDescending(b => b.Year)
                .ToArray();

            if (!booksByGenre.Any())
            {
                return NotFound("No books by genre found.");
            }
            return Ok(booksByGenre);
        }

        [HttpPost("/books/{genreId}")]
        public IActionResult Create(int genreId, [FromBody]Book book)
        {
            _context.CreateBook(book, genreId);
            return CreatedAtAction("Create", book);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bookToRemove = _context.GetBook(id);
            if (bookToRemove == null)
            {
                return NotFound("Could not delete book as it was not Found");
            }

            _context.DeleteBook(id);
            return Ok($"Book is deleted - {bookToRemove.Name}");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Book book)
        {
            try
            {
                var bookToEdit = _context.GetBook(id);
                if (bookToEdit == null)
                {
                    return NotFound("Could not update book as it was not Found");
                }
                _context.UpdateBook(book);
                return Ok($"Updated book - {book.Name}");
            }
            catch(ArgumentNullException)
            {
                //ModelState.AddModelError("", "Unable to save changes. Try again later.");
                return NotFound("Book not Found");
            }
        }
    }
}