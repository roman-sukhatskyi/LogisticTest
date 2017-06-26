using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using UM_LOGISTIC_V1.Models.Base;

namespace UM_LOGISTIC_V1.Models.TransportationApplication
{
    public class TransportationApplication : Entity
    {
        public TransportationApplication()
        {
            Pictures = new List<TransportationPicture.TransportationPicture>();
        }
        public string Name { get; set; }
        public string ContactPhone { get; set; }
        public string SendAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime? CompleteDate { get; set; }
        //public virtual ShipmentType ShipmentType { get; set; }
        //[ForeignKey("ShipmentType")]
        //public long ShipmentTypeId { get; set; }
        public string ShipmentType { get; set; }
        public long ShipmentLength { get; set; }
        public long ShipmentWidth { get; set; }
        public long ShipmentHeight { get; set; }
        public long ShipmentCapacity { get; set; }
        public long ShipmentWeight { get; set; }
        public bool Filtered { get; set; }

        public virtual List<TransportationPicture.TransportationPicture> Pictures { get; set; }
    }
}