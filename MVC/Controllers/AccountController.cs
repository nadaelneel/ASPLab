using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository;
using ViewModel;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {
        AccountManger AccountManger;

        public AccountController(AccountManger accountManger)
        {
            AccountManger = accountManger;
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
    }
}
