using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.Security
{
    public class RoleAccessUnit
    {
        public int Role { get; set; }
        public AccessOperation AccessOperation { get; set; }
    }
}