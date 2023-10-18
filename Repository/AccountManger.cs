using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Repository
{
    public class AccountManger : Manger<User>
    {
        UserManager<User> userManager;
        SignInManager<User> signInManager;
        public AccountManger(MyDBContext db , UserManager<User> _userManager , SignInManager<User> _signInManager) : base(db)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
        }
        
        public async Task<IdentityResult> SignUp( UserSignUpViewModel userSignUp)
        {
            return await userManager.CreateAsync(userSignUp.ToModel(), userSignUp.Password);
        }

        public async Task<SignInResult> SingIn(UserSignInViewModel userSignIn)
        {
            return await signInManager.PasswordSignInAsync(userSignIn.UserName , userSignIn.Password ,userSignIn.RememberMe , false );
        }
        public async void SignOut()
        {
            await signInManager.SignOutAsync();
        }

        public List<UserViewModel> Get()
        {
            return GetAll().Select(u =>u.ToModel()).ToList();
        }
        public UserViewModel GetUser(string email)
        {
            return  Get().Where(i=>i.Email == email).FirstOrDefault();
        }
        public async Task<IdentityResult> ChangePassword(UserChangePasswordViewModel viewModel)
        {
            var user = await userManager.FindByIdAsync(viewModel.Id);
            if (user != null)
            {
                return await userManager.ChangePasswordAsync(user, viewModel.CurrentPassword, viewModel.NewPassword);
            }
            return IdentityResult.Failed(new IdentityError()
            {
                Description = "User Not Found"
            });
        }

        public async Task<IdentityResult> ChangeEmail(UserChangeEmailViewModel viewModel)
        {
            var user = await userManager.FindByIdAsync(viewModel.Id);
            if (user != null)
            {
                return await userManager.ChangeEmailAsync(user , viewModel.NewEmail , "" );

                
            }
            return IdentityResult.Failed(new IdentityError()
            {
                Description = "User Not Found"
            });
        }

        public async Task<string> GetForgotPasswordCode(string Email)
        {
            var user = await userManager.FindByEmailAsync(Email);
            if (user != null)
            {
                var code = await userManager.GeneratePasswordResetTokenAsync(user);
                return code;
            }
            return string.Empty;
        }
        public async Task<IdentityResult> ForgotPassword(UserForgotPasswordViewModel viewModel)
        {
            var user = await userManager.FindByEmailAsync(viewModel.Email);
            if (user != null)
            {
                return await userManager.ResetPasswordAsync(user, viewModel.Code, viewModel.NewPassword);
            }
            return IdentityResult.Failed(new IdentityError()
            {
                Description = "User Not Found"
            });
        }
        public async Task<IdentityResult> AssignRolesToUser(string email, string role)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return await userManager.AddToRoleAsync(user, role);
            }
            return new IdentityResult();
        }

        //public async Task<IdentityResult> AssignRolesToUser(string email, string role)
        //{
        //    var user = await userManager.FindByEmailAsync(email);
        //    if (user != null)
        //    {
        //        return await userManager.AddToRoleAsync(user, role);
        //    }
        //    return new IdentityResult();
        //}

    }
}
