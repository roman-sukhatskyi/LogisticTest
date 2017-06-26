using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.Request.ClientTask
{
    public class ApplicationTaskRequest
    {
        public long ApplicationId { get; set; }
        public long? UserId { get; set; }
        public long TypeId { get; set; }
    }
}