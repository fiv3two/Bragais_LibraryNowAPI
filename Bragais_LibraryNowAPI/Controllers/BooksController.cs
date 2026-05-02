using Bragais_LibraryNowAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Bragais_LibraryNowAPI.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "The Metamorphosis",
                Author = "Franz Kafka",
                Genre = "Existentialism, Absurdism, Fiction",
                Available = true,
                PublishedYear = 1915
            },
            new Book
            {
                Id = 2,
                Title = "White Nights",
                Author = "Fyodor Dostoevsky",
                Genre = "First-person Narrative, Romance",
                Available = true,
                PublishedYear = 1848
            }
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new
            {
                status = "success",
                data = books,
                message = "Books Retrieved."
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not Found."
                });
            }
                
                return Ok(new
                {
                    status = "success",
                    data = book,
                    message = "Book Retrieved."
                });
        }

        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            newBook.Id = books.Count + 1;
            books.Add(newBook);
            return CreatedAtAction(nameof(GetById),
                new { id = newBook.Id },
                new
                {
                    status = "Success",
                    data = newBook,
                    message = "Book Created."
                });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,
            [FromBody] Book updateBook)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                    {
                        status = "error",
                        data = (object?)null,
                        message = "Book not Found."
                    });
            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            book.Genre = updateBook.Genre;
            book.Available = updateBook.Available;
            book.PublishedYear  = updateBook.PublishedYear;
            return Ok(new
            {
                status = "success",
                data = book,
                message = "Book Updated."
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault( x => x.Id == id );
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not Found."
                });
            books.Remove(book);
            return Ok(new
            {
                status = "success",
                data = (object?)null,
                message = "Book Deleted."
            });
        }
    }
}
