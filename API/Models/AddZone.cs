using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class AddZone
    {
        public int IdZone { get; set; }
        public string ZoneName { get; set; }
        public int City { get; set; }
        public string WellKnownValue { get; set; }
        public int CoordinateSystemId { get; set; }
        public System.Data.Entity.Spatial.DbGeography polygon { get; set; }
}
}