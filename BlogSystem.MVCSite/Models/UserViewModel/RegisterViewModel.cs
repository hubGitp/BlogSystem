using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSystem.MVCSite.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="邮箱")]
        public string Eamil { get; set; }
        [Required]
        [StringLength(maximumLength:50,MinimumLength =6)]
        [Display(Name ="密码")]
        public string Password { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [Required]
        [Compare(otherProperty:nameof(Password))]
        [Display(Name ="确认密码")]
        public string ConfirmPassword { get; set; }
    }
}