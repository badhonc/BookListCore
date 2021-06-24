using BookListM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListM.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BookListContext _db;

        [BindProperty]
        public Category Category { get; set; }

        public CategoryController(BookListContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Category = new Category();
            if (id == null)
            {
                //create
                return View(Category);
            }
            //update
            Category = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (Category == null)
            {
                return NotFound();
            }
            return View(Category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (Category.Id == 0)
                {
                    //create
                    _db.Categories.Add(Category);
                }
                else
                {
                    _db.Categories.Update(Category);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Category);
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Categories.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var catFromDb = await _db.Categories.FirstOrDefaultAsync(u => u.Id == id);
            if (catFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Categories.Remove(catFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}
