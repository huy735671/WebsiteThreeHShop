//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebsiteThreeHShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class THANHTOAN
    {
        public int MATT { get; set; }
        public Nullable<int> MAGH { get; set; }
        public Nullable<int> MASP { get; set; }
        public Nullable<int> MAKH { get; set; }
        public Nullable<double> GIACA { get; set; }
        public string PHUONGTHUCTHANHTOAN { get; set; }
        public System.DateTime NGAYTHANHTOAN { get; set; }
    
        public virtual KHACHHANG KHACHHANG { get; set; }
    }
}
