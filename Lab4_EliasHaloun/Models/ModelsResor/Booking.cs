//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lab4_EliasHaloun.Models.ModelsResor
{
    using System;
    using System.Collections.Generic;
    
    public partial class Booking
    {
        public int Id { get; set; }
        public string UserRef { get; set; }
        public int Amount { get; set; }
        public bool IsPaid { get; set; }
        public Nullable<int> Flight_Id { get; set; }
        public Nullable<int> RentACar_Id { get; set; }
    
        public virtual Flight Flight { get; set; }
        public virtual RentACar RentACar { get; set; }
    }
}
