using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.ApiModels.Filter
{
    public static class Parser
    {
        public static List<Filter> GetFilterList(string filter)
        {
            var list = new List<Filter>();
            if(filter == null)
            {
                return list;
            }
            var filters = filter.Split(';');
            foreach (var f in filters)
            {
                foreach (var operation in AvaibleOperations.avaibleSplitedOperations)
                {
                    var value = f.Split(new string[] { operation }, StringSplitOptions.RemoveEmptyEntries);
                    if (value.Length > 1)
                    {
                        list.Add(new Filter()
                        {
                            column = value[0],
                            value = value[1],
                            operation = operation
                        });
                    }
                }
            }
            return list;
        }
    }
}