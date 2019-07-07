using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspiritoTestWebApi.Models;
using InspiritoTestWebApi.Controllers;
using Microsoft.EntityFrameworkCore;

namespace InspiritoTestWebApi.Helpers
{
    public interface IRepository : IDisposable
    {
        List<Book> GetBooks();
        Book GetBookById(int id);
        void Create(Book item);
        void Update(Book item);
        void Delete(int id);
        void Save();
    }

    public class BookRepository : IRepository
    {
        private readonly BooksContext _context;
        public BookRepository(BooksContext context)
        {
            _context = context;
        }
        public List<Book> GetBooks()
        {
            return _context.Books.ToList();
        }
        public Book GetBookById(int id)
        {
            return _context.Books.Find(id);
        }

        public void Create(Book b)
        {
            _context.Books.Add(b);
        }

        public void Update(Book b)
        {
            _context.Entry(b).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Book b = _context.Books.Find(id);
            if (b != null)
                _context.Books.Remove(b);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
