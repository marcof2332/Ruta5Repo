//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shippments
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shippments()
        {
            this.Packages = new HashSet<Packages>();
            this.ShippmentStage = new HashSet<ShippmentStage>();
        }
    
        public int IdShippment { get; set; }
        public System.DateTime ReceiptDate { get; set; }
        public long Sender { get; set; }
        public string Recipient { get; set; }
        public string RecipientCel { get; set; }
        public int TargetZone { get; set; }
        public string TargetAddress { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    
        public virtual Customers Customers { get; set; }
        public virtual DropOffPackage DropOffPackage { get; set; }
        public virtual HomePickup HomePickup { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Packages> Packages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShippmentStage> ShippmentStage { get; set; }
        public virtual Zones Zones { get; set; }
    }
}