using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.Request.User
{
    public class UpdateUserRequest : BaseRequest
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }

        public long Id { get; set; }
        public long AccountId { get; set; }
        public long RoleId { get; set; }
    }
}