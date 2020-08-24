using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CookieAuthMVC5.ViewModel
{
    public class LoginViewModel
    {
        public string returnUrl { get; set; }
        [Required(ErrorMessage = "帳號是必填欄位")]
        public string Account { get; set; }
        [Required(ErrorMessage = "密碼是必填欄位")]
        public string Watchword { get; set; }
    }
}