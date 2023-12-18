using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.DTOs
{
    public class Login
    {
        public class Register
        {
            public string UserName { get; set; }     
            public string Email { get; set; }
            [Required(ErrorMessage = "Password is Required")]
            public string Password { get; set; }
        }
    }
}
