﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyHocSinh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class CTHP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CTHP()
        {
            this.KETQUAs = new HashSet<KETQUA>();
        }
        [DisplayName("Mã học phần")]
        public string MaCTHP { get; set; }
        [DisplayName("Học sinh")]
        public string MaHS { get; set; }
        [DisplayName("Môn học")]
        public string MaMH { get; set; }
        [DisplayName("Năm học")]
        public string MaNH { get; set; }
        [DisplayName("Giáo viên")]
        public string MaGV { get; set; }
    
        public virtual GIAOVIEN GIAOVIEN { get; set; }
        public virtual HOCSINH HOCSINH { get; set; }
        public virtual MONHOC MONHOC { get; set; }
        public virtual NAMHOC NAMHOC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KETQUA> KETQUAs { get; set; }
    }
}
