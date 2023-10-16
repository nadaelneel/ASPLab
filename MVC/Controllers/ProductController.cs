
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;
using ViewModel;

namespace MVC.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        public ProductManger ProductManger;
        public CategoryManger CategoryManger;
        public UniteOfWork UniteOfWork;

        public ProductController(ProductManger _productManger,
                                 CategoryManger _categoryManger, 
                                 UniteOfWork _uniteOfWork) 
        { 
            ProductManger = _productManger;
            CategoryManger = _categoryManger;
            UniteOfWork = _uniteOfWork;
        }
        public IActionResult GetAll()
        {
            ViewData["Products"] = ProductManger.Get().ToList();
            return View("GetAll");
        }

        public IActionResult GetDetails(int id)
        {
            ViewData["Product"] = ProductManger.GetProductById(id);
            return View();
        }

        public IActionResult AddForm() {
            ViewData["Categories"] = GetCategories ();
            return View();
        }

        public IActionResult AddProduct(AddProductViewModel addProduct) {


           
                foreach (IFormFile file in addProduct.Images)
                {
                    FileStream fileStream = new FileStream(
                        Path.Combine(
                            Directory.GetCurrentDirectory(), "Content", "Images", file.FileName),
                        FileMode.Create);
                    file.CopyTo(fileStream);
                    fileStream.Position = 0;
                    addProduct.ImageUrl.Add(file.FileName);
                   
                }
            
                ProductManger.Add(addProduct.AddtoModel());
            UniteOfWork.Save();
            return RedirectToAction("GetAll");
        }
        public IActionResult EditForm(int id)
        {
            ViewData["Categories"] = GetCategories();
            ViewData["product"] = ProductManger.GetProductById(id);
            return View("EditForm");
        }
        public IActionResult EditProduct(AddProductViewModel editProduct )
        {

            //foreach (IFormFile file in editProduct.Images)
            //{
            //    FileStream fileStream = new FileStream(
            //        Path.Combine(
            //            Directory.GetCurrentDirectory(), "Content", "Images", file.FileName),
            //        FileMode.Create);
            //    file.CopyTo(fileStream);
            //    fileStream.Position = 0;
            //    editProduct.ImageUrl.Add(file.FileName);
            //}

            ProductManger.Edit(editProduct);
            UniteOfWork.Save();
            return RedirectToAction("GetAll");
        }
        public ActionResult Delete(int id) {

            //ProductViewModel product = ProductManger.GetProductById(id);
            ProductManger.Delete(id);
            UniteOfWork.Save();
            return RedirectToAction("GetAll");
        }

        public IActionResult search(
            int ID,
            string Name ,
            double Price ,
            string CategoryName,
            string orderBy  ="ID" ,
            bool isAssending = true ,
            int PageSize = 3,
            int Pageindex = 1)
        {
            ViewData["Categories"] = GetCategories();
            
            var data = ProductManger.Search(ID, Name, Price, CategoryName, orderBy,isAssending,PageSize,Pageindex);

            return View(data);
        }
        private List<SelectListItem> GetCategories()
        {
            return CategoryManger.GetAll().Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.ID.ToString(),
            }).ToList();
        }
    }
}
