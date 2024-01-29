using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class LoginModel
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string token { get; set; }
    }
}