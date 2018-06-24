using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Yeast.Model.FrontEnd
{
    public class LoginModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resources),ErrorMessageResourceName = "UsernameIsRequired")]
        public string Username { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resources),ErrorMessageResourceName = "PasswordIsRequired")]
        public string Password { get; set; }

        public bool RemmemberMe { get; set; }
    }
}
