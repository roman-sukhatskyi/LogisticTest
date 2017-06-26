using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.Request.Account
{
    public class AddAccountAndLoginRequest : BaseRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmationPassword { get; set; }
        public string FullName { get; set; }
        public string WorkPhone { get; set; }
        public string City { get; set; }
        public long RoleId { get; set; }
    }
}