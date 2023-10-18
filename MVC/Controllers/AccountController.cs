using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using ViewModel;
using Models;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;

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
        public IActionResult AddRoleToUser2()
        {
            ViewData["Users"] = AccountManger.Get();
            return View();
        }

        [HttpGet]
        public IActionResult AddRoleToUser()
        {
            ViewData["Users"] = AccountManger.Get();

            return View();

        }


        [HttpPost]
        public async Task<IActionResult> AddRoleToUser(string email , string role)
        {
            if (ModelState.IsValid)
            {
                var result = await AccountManger.AssignRolesToUser(email, role);
                if(result.Succeeded)
                {
                    return RedirectToAction("AddRoleToUser2");
                }
            }
            

            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {

            ViewBag.Success = false;
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(UserChangePasswordViewModel viewModel)
        {
            viewModel.Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                var result = await AccountManger.ChangePassword(viewModel);
                if (result.Succeeded)
                {
                    ViewBag.Success = true;
                }
                return View();
            }
            ViewBag.Success = false;
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            ViewBag.Success = false;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (!string.IsNullOrEmpty(Email))
            {
                string code = await AccountManger.GetForgotPasswordCode(Email);
                if (string.IsNullOrEmpty(code))
                {
                    ViewBag.Success = false;
                }
                else
                {
                    //Send Mail With Code
                    var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
                    {
                        Credentials = new NetworkCredential("f8f4b090146a2c", "ba1c9259968370"),
                        EnableSsl = true
                    };
                     client.Send("from@example.com", Email, "Forget Password Verification", $"Your Code is {code}");
                    ViewBag.Success = true;
                }
                return View();
            }
            ViewBag.Success = false;
            return View();
        }
        [HttpGet]
        public IActionResult ForgotPasswordVerification()
        {
            ViewBag.Success = false;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPasswordVerification(UserForgotPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await AccountManger.ForgotPassword(viewModel);
                if (!result.Succeeded)
                {
                    ViewBag.Success = false;
                }
                else
                {
                    ViewBag.Success = true;
                }
                return View();
            }
            ViewBag.Success = false;
            return View();
        }

        [HttpGet]
        
        public IActionResult ChangeEmail()
        {
            ViewBag.Success = false;
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

