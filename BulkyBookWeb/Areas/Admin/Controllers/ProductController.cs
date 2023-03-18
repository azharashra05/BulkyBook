using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _db;
        public ProductController(IUnitOfWork db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _db.CoverType.GetAll();
            return View(objCoverTypeList);
        }

        
        //Get
        public IActionResult Upsert(int? id)
        {
            Product product = new Product();
            if (id == null || id == 0)
            {
                //create 
                return View(product);
            }
            else
            {
                //update product
            }
           
            return View(product);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if (coverType.Name.Length <=3)
            {
                ModelState.AddModelError("name", "The CoverType should be minimum length 3");
            }
            if (ModelState.IsValid)
            {
                _db.CoverType.Update(coverType);
                _db.Save();
                TempData["success"] = "CoverType updated successfully";
                return RedirectToAction("Index");
            }
            return View(coverType);

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var coverTypeFromDb = _db.CoverType.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbFirst=_db.Categories.FirstOrDefault(x=> x.Id == id);
            //var catefryFromDbSingle=_db.Categories.SingleOrDefault(x=> x.Id == id);
            if (coverTypeFromDb == null)
            {
                return NotFound();
            }
            return View(coverTypeFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(CoverType coverType)
        {
            _db.CoverType.Remove(coverType);
            _db.Save();
            TempData["success"] = "CoverType deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
