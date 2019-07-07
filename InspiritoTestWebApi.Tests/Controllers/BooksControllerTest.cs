using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using InspiritoTestWebApi.Models;
using InspiritoTestWebApi.Helpers;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using Nest;

namespace InspiritoTestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [TestClass]
    public class BooksControllerTest : ControllerBase
    {
        private readonly BooksContext _context;
        // GET api/values
        public BooksControllerTest(BooksContext context)
        {
            _context = context;

            if (_context.Books.Count() == 0)
            {
                _context.Books.Add(new Book { Author = "AuthorExample", Title = "TitleExample" });
                _context.SaveChanges();
            }
        }
        #region get_all_list
        [TestMethod]
        [HttpGet]
        public ActionResult<List<Book>> GetAll()
        {

            var mock = new Mock<IRepository>();
            mock.Setup(a => a.GetBooks()).Returns(new List<Book>());
            //BooksController controller = new BooksController(mock.Object);

            return _context.Books.ToList();
        }
        #endregion

        #region get_by_Id
        // GET api/values/ by Id
        [TestMethod]
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
        [TestMethod]
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
        [TestMethod]
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
        [TestMethod]
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
