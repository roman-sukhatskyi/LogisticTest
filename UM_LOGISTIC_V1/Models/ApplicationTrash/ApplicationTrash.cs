using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using UM_LOGISTIC_V1.Models.Base;

namespace UM_LOGISTIC_V1.Models.ApplicationTrash
{
    public class ApplicationTrash : Entity
    {
        [ForeignKey("User")]
        public long UserId { get; set; } 
        public virtual User.User User { get; set; }

        [ForeignKey("CooperationApplication")]
        public long? CooperationApplicationId { get; set; }
        public virtual CooperationApplication.CooperationApplication CooperationApplication { get; set; }

        [ForeignKey("TransportationApplication")]
        public long? TransportationApplicationId { get; set; }
        public virtual TransportationApplication.TransportationApplication TransportationApplication { get; set; }
    }
}