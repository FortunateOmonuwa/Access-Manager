
using System.ComponentModel.DataAnnotations;


namespace Domain.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Firstname is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastname is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Username is Required")]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
