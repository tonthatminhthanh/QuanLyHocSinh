//------------------------------------------------------------------------------
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
    
    public partial class GIAOVIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GIAOVIEN()
        {
            this.CTHPs = new HashSet<CTHP>();
            this.LOPs = new HashSet<LOP>();
        }
    
        public string MaGV { get; set; }
        public string HoGV { get; set; }
        public string TenGV { get; set; }
        public bool GioiTinh { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHP> CTHPs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOP> LOPs { get; set; }
    }
}
