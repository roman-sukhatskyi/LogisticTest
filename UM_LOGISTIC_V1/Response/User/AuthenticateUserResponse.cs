using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.Response.User
{
    public class AuthenticateUserResponse : BaseResponse
    {
        public string Token { get; set; }
        public Models.User.User Result { get; set; }
    }
}