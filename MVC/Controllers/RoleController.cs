using Microsoft.AspNetCore.Mvc;
using Repository;
using ViewModel;

namespace MVC.Controllers
{
    public class RoleController : Controller
    {
        RoleManager roleManager;
        public RoleController(RoleManager _roleManager)
        {
            roleManager = _roleManager;
        }

       

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Success = 0;
            ViewData["Data"] = roleManager.GetAll().Select(i => new AddRoleViewModle()
            {
                ID = i.Id,
                Name = i.Name
            }).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRoleViewModle veiwModel)
        {

            if (ModelState.IsValid)
            {
                var result = await roleManager.Add(veiwModel);
                if (result.Succeeded)
                {
                    ViewBag.Success = 1;
                }
                else
                {
                    ViewBag.Success = 2;
                }
            }
            else
            {
                ViewBag.Success = 2;
            }
            ViewData["Data"] = roleManager.GetAll().Select(i => new AddRoleViewModle()
            {
                ID = i.Id,
                Name = i.Name
            }).ToList();
            return View();

        }
    }
}
