using BookListM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListM.Controllers
{
    public class DemoController : Controller
    {
        private readonly BookListContext _db;

        [BindProperty]
        public Book Demo { get; set; }

        public DemoController(BookListContext db)
        {
            _db = db;
        }

        public IActionResult DemoCreate()
        {
            return View();
        }

        public IActionResult Upsert2(int? id)
        {
            Demo = new Book();
            if (id == null)
            {
                //create
                return View(Demo);
            }
            //update
            Demo = _db.Books.FirstOrDefault(u => u.Id == id);
            if (Demo == null)
            {
                return NotFound();
            }
            return View(Demo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert2()
        {
            if (ModelState.IsValid)
            {
                if (Demo.Id == 0)
                {
                    //create
                    _db.Books.Add(Demo);
                }
                else
                {
                    _db.Books.Update(Demo);
                }
                _db.SaveChanges();
                return RedirectToAction("DemoCreate");
            }
            return View(Demo);
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Books.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await _db.Books.FirstOrDefaultAsync(u => u.Id == id);
            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Books.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}
