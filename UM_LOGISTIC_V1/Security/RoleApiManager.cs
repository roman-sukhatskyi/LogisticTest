using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.Security
{
    public static class RoleApiManager
    {
        public static bool CheckAccess(Operation operation, int role, Section section)
        {
            var roleAccesses = RolesConfig.RoleAccessUnits.Where(ac => ac.Role == role);
            var roleOperations = roleAccesses.Where(ro => ro.AccessOperation.Section == section && ro.AccessOperation.Operations.Contains(operation));
            return roleOperations.Count() > 0;
        }
    }
}