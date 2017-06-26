using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.Response.CooperationApplication
{
    public class DeleteCooperationApplicationResponse : BaseResponse
    {
        public UM_LOGISTIC_V1.Models.CooperationApplication.CooperationApplication Result { get; set; }
    }
}