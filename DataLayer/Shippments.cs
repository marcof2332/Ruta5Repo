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
            this.HomePickups = new HashSet<HomePickups>();
            this.Packages = new HashSet<Packages>();
            this.ShippmentStages = new HashSet<ShippmentStages>();
        }
    
        public int IdShippment { get; set; }
        public System.DateTime ReceiptDate { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string RecipientCel { get; set; }
        public int TargetZone { get; set; }
        public string TargetAddress { get; set; }
        public System.Data.Entity.Spatial.DbGeography TargetLocation { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public virtual Customers Customers { get; set; }
        public virtual Customers Customers1 { get; set; }
        public virtual DropOffPackages DropOffPackages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HomePickups> HomePickups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Packages> Packages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShippmentStages> ShippmentStages { get; set; }
    }
}
