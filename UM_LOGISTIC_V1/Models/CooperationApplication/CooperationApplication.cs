using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using UM_LOGISTIC_V1.Models.Base;

namespace UM_LOGISTIC_V1.Models.CooperationApplication
{
    public class CooperationApplication : Entity
    {
        public CooperationApplication()
        {
            Pictures = new List<CooperationPicture.CooperationPicture>();
        }
        public string FullName { get; set; }
        public string ResidenceAddress { get; set; }
        public string ParkingPlace { get; set; }
        public string ContactPhone { get; set; }
        public bool IsPhysicalPerson { get; set; }
        public bool IsBussinessPerson { get; set; }
        public string CarModel { get; set; }
        public int TransportLength { get; set; }
        public int TransportWidth { get; set; }
        public int TransportHeight { get; set; }
        public int TransportWeight { get; set; }
        public int TransportCapacity { get; set; }
        public int TransportArrow { get; set; }
        public decimal WorkCost { get; set; }
        [ForeignKey("WorkType")]
        public long WorkTypeId { get; set; }
        public virtual ApplicationWorkType WorkType { get; set; }
        public decimal DeliveryCost { get; set; }
        public bool Filtered { get; set; }

        public virtual List<CooperationPicture.CooperationPicture> Pictures { get; set; }
    }
}