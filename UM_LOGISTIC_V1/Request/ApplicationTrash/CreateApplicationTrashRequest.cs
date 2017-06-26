using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.Request.ApplicationTrash
{
    public class CreateApplicationTrashRequest
    {
        public long UserId { get; set; }
        public long ApplicationId { get; set; }
        public bool Type { get; set; }
    }
}