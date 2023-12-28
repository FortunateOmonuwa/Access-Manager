
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class UserEntity : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //[Key]
        //[DisplayName("User ID")]
        //public int UserId { get; set; }

        //[DisplayName("Username")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "The username is required!")]
        //[StringLength(maximumLength: 50, MinimumLength = 8, ErrorMessage = "The username has to be between 8 and 50 characters+")]
        //public string Username { get; set; }

        //[DisplayName("Email")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "The username is required!")]
        //[DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email address!")]
        //public string Email { get; set; }


        //[DisplayName("Password")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "The password is required!")]
        //[StringLength(maximumLength: 50, MinimumLength = 8, ErrorMessage = "The password has to be between 8 and 50 characters+")]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        //[DisplayName("Age")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "The age is required!")]
        //[Range(minimum: 18, maximum: 100, ErrorMessage = "The age has to be betwen 18 and 100.")]
        //public int Age { get; set; }

        //[DisplayName("Registration date")]
        //public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
