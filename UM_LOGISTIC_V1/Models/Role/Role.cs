using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UM_LOGISTIC_V1.Models.Base;

namespace UM_LOGISTIC_V1.Models.Role
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public short Number { get; set; }
    }
}