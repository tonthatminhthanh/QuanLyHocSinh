using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLHocSinhHVT.Models
{
    public class RoleSelectionModel
    {
        public UserRolesViewModel user { get; set; }
        public List<RolesViewModel> roles { get; set; }
    }
}