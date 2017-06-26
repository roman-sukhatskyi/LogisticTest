using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.Request.ClientTask
{
    public class CallFeedbackRequest
    {
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Question { get; set; }

        public long? UserId { get; set; }
    }
}