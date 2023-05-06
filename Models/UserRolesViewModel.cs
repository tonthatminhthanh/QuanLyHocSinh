using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHocSinhHVT.Models
{
    public class UserRolesViewModel
    {
        public string userId { get; set; }
        public string email { get; set; }
        public List<String> roles { get; set; }
    }
}