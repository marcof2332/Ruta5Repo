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
    
    public partial class Vehicles
    {
        public int IdVehicle { get; set; }
        public string Plate { get; set; }
        public string vRegistration { get; set; }
        public string BrandModel { get; set; }
        public decimal VehicleWeight { get; set; }
        public int Condition { get; set; }
    
        public virtual VehiclesCondition VehiclesCondition { get; set; }
    }
}
