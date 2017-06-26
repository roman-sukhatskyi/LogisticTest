using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.Response.Account
{
    public class UpdateAccountResponse : BaseResponse
    {
        public UM_LOGISTIC_V1.Models.Account.Account Result { get; set; }
    }
}