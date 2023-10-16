using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class UserSignUpViewModel
    {
        [Required , StringLength (50,ErrorMessage ="Must be less than 50 and more than 3 char", MinimumLength =3)]
        public string FirstName { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]

        public string LastName { get; set; }

        [Required, StringLength(14, MinimumLength = 14)]
        public string NationalID { get; set; }

        public string Picture { get; set; } = "notFound.jpg";

        [Required, StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required, StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(20 , MinimumLength =5)]

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, StringLength(20, MinimumLength = 5)]

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
