using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSystem.MVCSite.Models.LoginViewModel
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="电子邮箱")]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength:50,MinimumLength =6)]
        [Display(Name ="密码")]
        [DataType(dataType:DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="记住我")]
        public bool RememberMe { get; set; }
    }
}