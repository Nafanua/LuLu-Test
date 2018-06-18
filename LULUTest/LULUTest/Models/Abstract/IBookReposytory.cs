using System.Collections.Generic;
using System.Threading.Tasks;

namespace LULUTest.Models.Abstract
{
    public interface IBookReposytory
    {
        Task<BookModel> GetBookByIdAsync(int id);

        Task<IEnumerable<BookModel>> GetAllBooks();

        Task AddBookAsync(BookModel book);

        Task DeleteBookAsync(int id);

        Task EditBookAsync(BookModel book);

        void Dispose();
    }
}