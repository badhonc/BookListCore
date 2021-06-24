using BookListM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BookListM.Controllers
{
    public class SubcategoryController : Controller
    {
        private readonly BookListContext _db;
        db_acccess_layer.db dbo = new db_acccess_layer.db();

        [BindProperty]
        public Subcategory Subcategory { get; set; }

        public SubcategoryController(BookListContext db1)
        {
            _db = db1;
        }


        public IActionResult Index()
        {
            DataSet ds = dbo.GetCategory();
           // var categoryId = HttpContext.Request.Form["CategoryId"].ToString();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new SelectListItem { Text = dr["Name"].ToString(), Value = dr["Id"].ToString() });
            }
            ViewBag.CategoryList = list;
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            //Subcategory = new Subcategory();
            DataSet ds = dbo.GetCategory();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new SelectListItem { Text = dr["Name"].ToString(), Value = dr["Id"].ToString() });
            }
            ViewBag.CategoryList = list;
            if (id == null)
            {
                //create
                return View(Subcategory);
            }
            //update
            Subcategory = _db.Subcategories.FirstOrDefault(u => u.Id == id);
            if (Subcategory == null)
            {
                return NotFound();
            }
            return View(Subcategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {

            if (ModelState.IsValid)
            {
                if (Subcategory.Id == 0)
                {
                    //create
                    _db.Subcategories.Add(Subcategory);
                }
                else
                {
                    _db.Subcategories.Update(Subcategory);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Subcategory);
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Subcategories.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var catFromDb = await _db.Subcategories.FirstOrDefaultAsync(u => u.Id == id);
            if (catFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Subcategories.Remove(catFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}
