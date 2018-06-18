using AutoMapper;
using LULUTest.Models;
using LULUTest.Models.Abstract;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LULUTest.Controllers
{
    public class BookController : Controller
    {
        private IBookReposytory _repo;

        public BookController(IBookReposytory repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Index(int? page, string filter = null)
        {
            var pageSize = 16;
            var pageNumber = (page ?? 1);
            var books = await _repo.GetAllBooks();

            var viewBooks = Mapper.Map<IEnumerable<BookViewModel>>(books);

            if (filter == null || filter == string.Empty)
            {                
                return View(viewBooks.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return View(viewBooks.Where(b => b.Name.ToLower().Contains(filter.ToLower()) || 
                                                  b.Price.ToString().Contains(filter) || 
                                                  b.Author.ToLower().Contains(filter.ToLower()))
                                                  .ToPagedList(pageNumber, pageSize));
            }
            
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Details(int id)
        {
            var dataBook = await _repo.GetBookByIdAsync(id);

            var book = Mapper.Map<BookViewModel>(dataBook);

            if (book == null)
            {
                return RedirectToAction("Index");
            }

            return View(book);
        }

        [HttpPost]
        [Authorize (Roles = "seller")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            await _repo.DeleteBookAsync(id);

            return RedirectToAction("Index");
        }

        [Authorize (Roles = "seller")]
        public ActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "seller")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddBook(BookViewModel book)
        {
            if (ModelState.IsValid)
            {
                var dataBook = Mapper.Map<BookModel>(book);

                await _repo.AddBookAsync(dataBook);

                return RedirectToAction("Index");
            }

            return View(book);
        }

        [HttpGet]
        [Authorize(Roles ="seller")]
        public async Task<ActionResult> EditBook(int id)
        {
            var dataBook = await _repo.GetBookByIdAsync(id);

            var book = Mapper.Map<BookViewModel>(dataBook);

            return View(book);
        }

        [HttpPost]
        [Authorize(Roles ="seller")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditBook(BookViewModel book)
        {
            if (ModelState.IsValid)
            {
                var dataBook = Mapper.Map<BookModel>(book);

                await _repo.EditBookAsync(dataBook);

                return RedirectToAction("Index");
            }

            return View(book);            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_repo != null)
                {
                    _repo.Dispose();
                    _repo = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}