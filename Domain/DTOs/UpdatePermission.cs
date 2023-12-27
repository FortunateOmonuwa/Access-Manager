using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.DTOs
{
    public class UpdatePermission
    {
        [Required(ErrorMessage = "Username is Required")]
        public string UserName { get; set; }
    
    }
}
