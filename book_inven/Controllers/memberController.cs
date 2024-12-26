using book_inven.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace book_inven.Controllers
{
    public class memberController : Controller
    {
        private readonly BooksInvenContext booksContext;
        public memberController(BooksInvenContext context)
        {
            booksContext = context;
        }
        public IActionResult Index()
        {
            ICollection<Member> members = booksContext.Members.ToList();
            return View(members);
        }
        public IActionResult create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult create(Member member)
        {
            if (ModelState.IsValid)
            {
                booksContext.Members.Add(member);
                booksContext.SaveChanges();
                return RedirectToAction("Index");
            }
            // If the model is invalid, return the same view with the current model for error display

            return View(member);
        }
        public IActionResult Delete(int id)
        {
            var Dmember = booksContext.Members.Where(x => x.Mid == id).FirstOrDefault();
            booksContext.Members.Remove(Dmember);
            booksContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public decimal CalculateFine(DateOnly borrowDate, DateOnly? returnDate, decimal dailyFine)
        {
            var actualReturnDate = returnDate ?? DateOnly.FromDateTime(DateTime.Now);

            if (actualReturnDate <= borrowDate)
            {
                return 0; 
            }

            var overdueDays = (actualReturnDate.ToDateTime(TimeOnly.MinValue) - borrowDate.ToDateTime(TimeOnly.MinValue)).Days;

            Console.WriteLine($"Borrow Date: {borrowDate}, Actual Return Date: {actualReturnDate}, Overdue Days: {overdueDays}");

            return overdueDays * dailyFine; 
        }

        public IActionResult GetFineStatus(int memberId)
        {
            var memberActions = booksContext.Memberactions
                                           .Where(ma => ma.Mid == memberId && ma.ReturnDate == null)
                                           .ToList();

            decimal totalFine = 0;
            decimal dailyFine = 5; 

            Console.WriteLine($"Calculating fine for member with ID: {memberId}");

            foreach (var action in memberActions)
            {
                decimal fineForAction = CalculateFine(action.BorrowDate, null, dailyFine);
                totalFine += fineForAction;

                Console.WriteLine($"Book borrowed on {action.BorrowDate}, Fine: {fineForAction}");
            }

            Console.WriteLine($"Total Fine for Member {memberId}: {totalFine}");

            return Json(new { hasFine = totalFine > 0, fineAmount = totalFine });
        }


        public IActionResult Edit(int id)
        {
            var member = booksContext.Members.Where(s => s.Mid == id).FirstOrDefault();
            return View(member);
        }
        [HttpPost]
        public IActionResult Edit(Member member)
        {
            var currentmember = booksContext.Members.Where(s => s.Mid == member.Mid).FirstOrDefault();
            currentmember.Mid = member.Mid;
            currentmember.Mname = member.Mname;
            currentmember.Maddress = member.Maddress;
            currentmember.Mphone = member.Mphone;



            booksContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var member = booksContext.Members
                .Include(m => m.Memberactions)    
                .ThenInclude(ma => ma.BidNavigation) 
                .FirstOrDefault(m => m.Mid == id);

            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }










    }
}
