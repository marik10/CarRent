//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarRent.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class MileageHistory
    {
        public int MileageId { get; set; }
        public int CarId { get; set; }
        public System.DateTime Date { get; set; }
        public int Mileage { get; set; }
    
        public virtual Cars Cars { get; set; }
    }
}