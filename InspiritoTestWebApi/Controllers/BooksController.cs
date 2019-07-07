using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using InspiritoTestWebApi.Models;

namespace InspiritoTestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksContext _context;
        // GET api/values
        public BooksController(BooksContext context)
        {
            _context = context;

            if (_context.Books.Count() == 0)
            {
                _context.Books.Add(new Book { Author = "AuthorExample", Title = "TitleExample" });
                _context.SaveChanges();
            }
        }
        #region get_all_list
        [HttpGet]
        public ActionResult<List<Book>> GetAll()
        {
            return _context.Books.ToList();
        } 
        #endregion

        #region get_by_Id
        // GET api/values/ by Id
        [HttpGet("{id}", Name ="GetBook")]
        public ActionResult<Book> GetById(int id)
        {
            var item = _context.Books.Find(id);

            if(item == null)
            {
                return NotFound();
            }
            return item;
        }
        #endregion

        #region Creates a books
        // POST api/values
        [HttpPost]
        public ActionResult<Book> Create(Book item)
        {
            _context.Books.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetBook", new { id = item.Id }, item);
        }
        #endregion
        #region Update
        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, Book item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var b = _context.Books.Find(id);

            if (b == null)
            {
                return NotFound();
            }

            b.Author = item.Author;
            b.Title = item.Title;
            b.Id = item.Id;

            _context.Books.Update(b);
            _context.SaveChanges();

            return NoContent();
        }
        #endregion
        #region Delete By Id
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var b = _context.Books.Find(id);

            if (b == null)
            {
                return NotFound();
            }

            _context.Books.Remove(b);
            _context.SaveChanges();

            return NoContent();
        }
        #endregion
    }
}
