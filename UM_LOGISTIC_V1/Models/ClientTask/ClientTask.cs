using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using UM_LOGISTIC_V1.Models.Base;

namespace UM_LOGISTIC_V1.Models.ClientTask
{
    public class ClientTask : Entity
    {
        public string Title { get; set; }
        [ForeignKey("Type")]
        public long TypeId { get; set; }
        public virtual ClientTaskType Type { get; set; }
		
		[ForeignKey("Owner")]
        public long OwnerId { get; set; }
        public virtual User.User Owner { get; set; }

        [ForeignKey("User")]
        public long? UserId { get; set; }
        public virtual User.User User { get; set; }

        [ForeignKey("CooperationApplication")]
        public long? CooperationApplicationId { get; set; }
        public virtual CooperationApplication.CooperationApplication CooperationApplication { get; set; }

        [ForeignKey("TransportationApplication")]
        public long? TransportationApplicationId { get; set; }
        public virtual TransportationApplication.TransportationApplication TransportationApplication { get; set; }
    }
}