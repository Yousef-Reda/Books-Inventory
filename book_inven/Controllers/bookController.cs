using book_inven.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace book_inven.Controllers
{
    public class bookController : Controller
    {
        private readonly BooksInvenContext booksContext;
        public bookController(BooksInvenContext context)
        {
            booksContext = context;
        }
        public IActionResult Index()
        {
            ICollection<Book> books = booksContext.Books.ToList();
            return View(books);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                booksContext.Books.Add(book);
                booksContext.SaveChanges();
                return RedirectToAction("Index");
            }

            // If the model is invalid, return the same view with the current model for error display
            return View(book);
        }
        public IActionResult Delete(int id)
        {
            var Dbook = booksContext.Books.Where(x => x.Bid == id).FirstOrDefault();
            booksContext.Books.Remove(Dbook);
            booksContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var book = booksContext.Books.Where(s => s.Bid == id).FirstOrDefault();
            return View(book);
        }
        [HttpPost]
        public IActionResult Edit(Book book)
        {
            var currentbook = booksContext.Books.Where(s => s.Bid == book.Bid).FirstOrDefault();
            currentbook.Bid = book.Bid;
            currentbook.Btitle = book.Btitle;
            currentbook.Bprice = book.Bprice;
            currentbook.Btype = book.Btype;

            booksContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var book = booksContext.Books
                      .FirstOrDefault(b => b.Bid == id);


            if (book == null)
            {
                Console.WriteLine("Book Not Found");
                return NotFound();
            }
            return View(book);
        }
     



    }
}
