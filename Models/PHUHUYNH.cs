﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLHocSinhHVT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class PHUHUYNH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHUHUYNH()
        {
            this.HOCSINHs = new HashSet<HOCSINH>();
        }
        [DisplayName("Mã phụ huynh")]
        public string MaPH { get; set; }
        public string HoPH { get; set; }
        public string TenPH { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string NgheNghiep { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOCSINH> HOCSINHs { get; set; }
    }
}