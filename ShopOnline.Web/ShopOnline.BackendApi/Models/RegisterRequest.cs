using System;
using System.ComponentModel.DataAnnotations;

namespace ShopOnline.BackendApi.Models
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare(otherProperty: "Password", ErrorMessage = "Confirm password does not match")]
        public string ConfirmPassword { get; set; }
    }
}