using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.Response.ClientTask
{
    public class GetClientTasksResponse : BaseResponse
    {
        public List<UM_LOGISTIC_V1.Models.ClientTask.ClientTask> Result { get; set; }
    }
}