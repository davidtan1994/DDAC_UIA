using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ExpressiveAnnotations.Attributes;

namespace UIA_Flight_Booking_System.ViewModels
{
    public class CustomerRegistrationViewModel
    {
        [Display(Name ="First Name")]
        [Required(ErrorMessage ="Please fill in first name")]
        public string Fname { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please fill in last name")]
        public string Lname { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please select gender")]
        public string Gender { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please fill in email")]
        public string Email { get; set; }

        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "Please fill in contact number")]
        public string ContactNum { get; set; }

        [Display(Name = "I.C Number")]
        [Required(ErrorMessage = "Please fill in IC number")]
        public string IcNum { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please fill in address")]
        public string Address { get; set; }

        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Please fill in date of birth")]
        public System.DateTime DOB { get; set; }
        public System.DateTime RegistrationDate { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please fill in username")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please fill in password")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [RequiredIf("Password != null", ErrorMessage = "Please confirm your password")]
        public string ConfirmPassword { get; set; }
    }
}