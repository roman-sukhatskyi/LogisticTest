using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.Request.CooperationApplication
{
    public class UpdateCooperationApplicationRequest : BaseRequest
    {
        public long Id { get; set; }
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
        public long WorkTypeId { get; set; }
        public decimal DeliveryCost { get; set; }
    }
}