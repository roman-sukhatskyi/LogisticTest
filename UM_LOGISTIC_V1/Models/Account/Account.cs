using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UM_LOGISTIC_V1.Models.Base;

namespace UM_LOGISTIC_V1.Models.Account
{
    public class Account : Entity
    {
        public string FullName { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Image { get; set; }
    }
}