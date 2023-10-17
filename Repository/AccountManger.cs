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
    }
}
