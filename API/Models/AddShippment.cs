using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;

namespace API.Models
{
    public class AddShippment
    {
        public DropOffPackage DoP { get; set; }
        public ShippmentStage ShStage { get; set; }

        #region Shippment
        public int IdShippment { get; set; }
        public DateTime ReceiptDate { get; set; }
        public long Sender { get; set; }
        public string Recipient { get; set; }
        public string RecipientCel { get; set; }
        public int TargetZone { get; set; }
        public string TargetAddress { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        #endregion
        #region DropOffPackage
        public int BranchOffice { get; set; }
        public string Note { get; set; }
        #endregion
        #region DropOffPackage
        public int IdSStage { get; set; }
        public int EmpID { get; set; }
        public System.DateTime DateTimeStage { get; set; }
        public Nullable<int> Vehicle { get; set; }
        #endregion
    }
}