using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }

        
        //Get
        public IActionResult Upsert(int? id)
        {
            Product product = new Product();
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u=>new SelectListItem
            {
                Text = u.Name,
                Value=u.Id.ToString()
            } );
            IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.CoverType.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            if (id == null || id == 0)
            {
                //create 
                ViewBag.CategoryList = CategoryList;
                ViewBag.CoverTypeList=CoverTypeList;
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
                _unitOfWork.CoverType.Update(coverType);
                _unitOfWork.Save();
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
            var coverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
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
            _unitOfWork.CoverType.Remove(coverType);
            _unitOfWork.Save();
            TempData["success"] = "CoverType deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
