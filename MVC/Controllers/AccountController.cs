using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using ViewModel;
using Models;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {
        AccountManger AccountManger;
        RoleManager RoleManager;
        public AccountController(AccountManger accountManger , RoleManager _RoleManager)
        {
            AccountManger = accountManger;
            RoleManager = _RoleManager;
        }

        [HttpGet]
        
        public IActionResult SignIn()
        {
            //var veiw = new UserSignInViewModel() { ReturnUrl = ReturnUrl };
            return View("SignIn");
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInViewModel userSignIn) {

            if (ModelState.IsValid)
            {
                var result = await AccountManger.SingIn(userSignIn);
                if (result.Succeeded )
                {
                    return RedirectToAction("GetAll", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Invaild User Name Or Password");
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View("SignUp");
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignUpViewModel userSignUp)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await AccountManger.SignUp(userSignUp);
                if (result.Succeeded)
                {
                    return RedirectToAction("SignIn");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        
        public IActionResult SignOut()
        {
            AccountManger.SignOut();
            return RedirectToAction("SignIn");
        }

        [HttpGet]
        public IActionResult AddRoleToUser()
        {
            ViewData["Users"] =  AccountManger.Get();

            return View();

        }
        [HttpPost]

        public IActionResult AddRoleToUser(string email , string role)
        {
            UserViewModel u = AccountManger.GetUser(email);

           u.Role = role;

            return View();
        }
        private List<SelectListItem> RoleList()
        {
            return RoleManager.GetAll().Select(r => new SelectListItem()
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();
        }
    }
}

