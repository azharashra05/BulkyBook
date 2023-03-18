using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _db;
        public CoverTypeController(IUnitOfWork db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _db.CoverType.GetAll();
            return View(objCoverTypeList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType coverType)
        {
            if (coverType.Name.Length <= 3)
            {
                ModelState.AddModelError("name", "The CoverType should be minimum length 3");
            }
            if (ModelState.IsValid)
            {
                _db.CoverType.Add(coverType);
                _db.Save();
                TempData["success"] = "CoverType created successfully";
                return RedirectToAction("Index");
            }
            return View(coverType);

        }

        public IActionResult Edit(int? id)
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
        public IActionResult Edit(CoverType coverType)
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
