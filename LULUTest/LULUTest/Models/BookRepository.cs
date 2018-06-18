using LULUTest.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace LULUTest.Models
{
    public class BookRepository : IBookReposytory, IDisposable
    {
        private ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BookModel> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<IEnumerable<BookModel>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task AddBookAsync(BookModel book)
        {
            if (book != null)
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task EditBookAsync(BookModel book)
        {
            if (book != null)
            {
                var existing = await _context.Books.FindAsync(book.Id);

                if (existing != null)
                {
                    _context.Entry(existing).CurrentValues.SetValues(book);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}