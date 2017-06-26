using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.Request.User
{
    public class AuthenticateUserRequest
    {
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }
}