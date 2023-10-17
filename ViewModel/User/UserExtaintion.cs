using Microsoft.CodeAnalysis.CSharp.Syntax;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public static class UserExtaintion
    {
        public static User ToModel(this UserSignUpViewModel viewModel)
        {
            return new User
            {

                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                NationalID = viewModel.NationalID,
                Picture = viewModel.Picture,
                UserName = viewModel.UserName,
            };
        }
        public static User ToModel(this UserViewModel viewModel)
        {
            return new User
            {

                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                NationalID = viewModel.NationalID,
                Picture = viewModel.Picture,
                UserName = viewModel.UserName,
                
            };
        }
        public static UserViewModel ToModel(this User viewModel)
        {
            return new UserViewModel
            {

                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                NationalID = viewModel.NationalID,
                Picture = viewModel.Picture,
                UserName = viewModel.UserName,

            };
        }
    }
}
