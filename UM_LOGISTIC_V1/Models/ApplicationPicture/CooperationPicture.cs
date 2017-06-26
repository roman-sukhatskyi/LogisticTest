using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using UM_LOGISTIC_V1.Models.Base;
using UM_LOGISTIC_V1.Models.CooperationApplication;

namespace UM_LOGISTIC_V1.Models.CooperationPicture
{
    public class CooperationPicture : Entity
    {
		[ForeignKey("CooperationApplication")]
        public long CooperationApplicationId { get; set; }
		public virtual CooperationApplication.CooperationApplication CooperationApplication { get; set; }
        public string Image { get; set; }
    }
}