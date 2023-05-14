using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyHocSinh.Models
{
    public class RolesViewModel
    {
        public RolesViewModel(string RoleId, string RoleName) { 
            this.RoleId = RoleId;
            this.RoleName = RoleName;
        }

        public RolesViewModel() { }

        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool selected { get; set; }
    }
}