using LULUTest.Models;
using LULUTest.Models.Abstract;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LULUTest.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private IBookReposytory _repo;
        private readonly IOrderProcessor _orderProcessor;

        public CartController(IBookReposytory repo, IOrderProcessor orderProcessor)
        {
            _repo = repo;
            _orderProcessor = orderProcessor;
        }

        [HttpGet]
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToRouteResult> AddToCart(Cart cart, int bookId)
        {
            var book = await _repo.GetBookByIdAsync(bookId);

            if (book != null)
            {
                cart.AddLine(book, 1);
            }

            return RedirectToAction("Index", "Book");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToRouteResult> RemoveBookFromCart(Cart cart, int bookId)
        {
            var book = await _repo.GetBookByIdAsync(bookId);

            if (book != null)
            {
                cart.RemoveBook(book);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToRouteResult> RemoveLineFromCart(Cart cart, int bookId)
        {
            var book = await _repo.GetBookByIdAsync(bookId);

            if (book != null)
            {
                cart.RemoveLine(book);
            }

            return RedirectToAction("Index");
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ActionResult Checkout()
        {
            ViewBag.result = false;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Checkout(Cart cart, OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.result = await _orderProcessor.ProcessOrder(cart, order);

                    if (ViewBag.result)
                    {
                        cart.Clear();
                    }

                    return View();
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", ex.Message);
                }                
            }

            return View(order);            
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