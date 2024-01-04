using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class addOffice
    {
        public int BranchZone { get; set; }
        public string BranchAddress { get; set; }
        public string Phone { get; set; }
        public System.TimeSpan OpTime { get; set; }
        public System.TimeSpan CloseTime { get; set; }
        public string WellKnownValue { get; set; }
        public int CoordinateSystemId { get; set; }
        public System.Data.Entity.Spatial.DbGeography marker { get; set; }
    }
}