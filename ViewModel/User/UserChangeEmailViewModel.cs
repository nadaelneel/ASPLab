using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class UserChangeEmailViewModel
    {
        public string Id { get; set; } = "";

        [Required, StringLength(50)]
        [EmailAddress]
        public string CurrentEmail { get; set; }

        [Required, StringLength(50)]
        [EmailAddress]
        public string NewEmail { get; set; }


    }
}
