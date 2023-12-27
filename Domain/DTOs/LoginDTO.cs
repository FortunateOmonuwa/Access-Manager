using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.DTOs
{
    public class LoginDTO
    {
        public string UsernameOrEmail { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
        
    }
}
