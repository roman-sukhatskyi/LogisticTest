using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.Response.User
{
    public class CreateUserResponse : BaseResponse
    {
        public UM_LOGISTIC_V1.Models.User.User Result { get; set; }
    }
}