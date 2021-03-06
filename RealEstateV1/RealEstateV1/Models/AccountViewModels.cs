﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateV1.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessage = "الرجاء ادخال الايميل")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {

        [Required(ErrorMessage = "الرجاء ادخال الايميل")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "الرجاء ادخال كلمة المرور")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {

        public string ID { get; set; }

        [Required(ErrorMessage = "الرجاء ادخال الايميل")]
        [EmailAddress]
        [Display(Name = "الايميل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "الرجاء ادخال كلمة المرور")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }

        [Required(ErrorMessage = "الرجاء تأكيد كلمة المرور")]
        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة المرور")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "الرجاء ادخال اسم المستخدم")]
        [Display(Name = "اسم المستخدم")]
        public string UserName { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "الرجاء ادخال الايميل")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class RegisterCustomerViewModel
    {
        public T_Customer Customer { get; set; }
        public RegisterViewModel Register { get; set; }
    }

    public class RegisterExternalCustomerViewModel
    {
        public T_Customer Customer { get; set; }
        public ExternalLoginConfirmationViewModel Register { get; set; }
    }
}
