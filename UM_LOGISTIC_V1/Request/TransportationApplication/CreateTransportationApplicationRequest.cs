using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.Request.TransportationApplication
{
    public class CreateTransportationApplicationRequest : BaseRequest
    {
        public string Name { get; set; }
        public string ContactPhone { get; set; }
        public string SendAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime? CompleteDate { get; set; }
        public string ShipmentType { get; set; }
        public long ShipmentLength { get; set; }
        public long ShipmentWidth { get; set; }
        public long ShipmentHeight { get; set; }
        public long ShipmentCapacity { get; set; }
        public long ShipmentWeight { get; set; }
        public long CreatedBy { get; set; }
        public string Image { get; set; }
    }
}