using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.Request.Account
{
    public class CreateAccountRequest : BaseRequest
    {
        public string FullName { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }
}