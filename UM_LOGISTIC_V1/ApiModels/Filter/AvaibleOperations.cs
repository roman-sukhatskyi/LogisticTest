using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.ApiModels.Filter
{
    public static class AvaibleOperations
    {
        public static Dictionary<string, OperatorComparer> operations = new Dictionary<string, OperatorComparer>();
        public static List<string> avaibleSplitedOperations = new List<string>();
        static AvaibleOperations()
        {
            operations.Add("%%", OperatorComparer.Contains);
            operations.Add("%=", OperatorComparer.StartsWith);
            operations.Add("=%", OperatorComparer.EndsWith);
            operations.Add("==", OperatorComparer.Equals);
            operations.Add(">>", OperatorComparer.GreaterThan);
            operations.Add("<<", OperatorComparer.LessThan);
            operations.Add(">=", OperatorComparer.GreaterThanOrEqual);
            operations.Add("<=", OperatorComparer.LessThanOrEqual);
            operations.Add("<>", OperatorComparer.NotEqual);

            avaibleSplitedOperations.Add("%%");
            avaibleSplitedOperations.Add("%=");
            avaibleSplitedOperations.Add("=%");
            avaibleSplitedOperations.Add("==");
            avaibleSplitedOperations.Add(">>");
            avaibleSplitedOperations.Add("<<");
            avaibleSplitedOperations.Add(">=");
            avaibleSplitedOperations.Add("<=");
            avaibleSplitedOperations.Add("<>");
        }
    }
}