using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.ApiModels.Filter
{
    public class Filter
    {
        public string column { get; set; }
        public object value { get; set; }
        public string operation { get; set; }
    }
}