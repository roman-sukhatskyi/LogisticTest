using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UM_LOGISTIC_V1.Models.User;

namespace UM_LOGISTIC_V1.Response.Account
{
    public class RegisterAccountResponse : BaseResponse
    {
        public Models.User.User Result { get; set; }
        public string Token { get; set; }
    }
}