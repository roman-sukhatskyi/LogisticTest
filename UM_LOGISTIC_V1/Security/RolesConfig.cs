using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.Security
{
    public static class RolesConfig
    {
        public static List<RoleAccessUnit> RoleAccessUnits = new List<RoleAccessUnit>();

        static RolesConfig()
        {
            RoleAccessUnits.Add(new RoleAccessUnit()
            {
                Role = 0,
                AccessOperation = new AccessOperation()
                {
                    Section = Section.Users,
                    Operations = new List<Operation>()
                    {
                        Operation.Read,
                        Operation.Create,
                        Operation.Update,
                        Operation.Remove
                    } 
                }
            });
            RoleAccessUnits.Add(new RoleAccessUnit()
            {
                Role = 0,
                AccessOperation = new AccessOperation()
                {
                    Section = Section.Accounts,
                    Operations = new List<Operation>()
                    {
                        Operation.Read,
                        Operation.Create,
                        Operation.Update,
                        Operation.Remove
                    }
                }
            });
            RoleAccessUnits.Add(new RoleAccessUnit()
            {
                Role = 0,
                AccessOperation = new AccessOperation()
                {
                    Section = Section.Roles,
                    Operations = new List<Operation>()
                    {
                        Operation.Read,
                        Operation.Create,
                        Operation.Update,
                        Operation.Remove
                    }
                }
            });
            RoleAccessUnits.Add(new RoleAccessUnit()
            {
                Role = 0,
                AccessOperation = new AccessOperation()
                {
                    Section = Section.CooperationApplications,
                    Operations = new List<Operation>()
                    {
                        Operation.Read,
                        Operation.Create,
                        Operation.Update,
                        Operation.Remove
                    }
                }
            });
            RoleAccessUnits.Add(new RoleAccessUnit()
            {
                Role = 0,
                AccessOperation = new AccessOperation()
                {
                    Section = Section.TransportationApplications,
                    Operations = new List<Operation>()
                    {
                        Operation.Read,
                        Operation.Create,
                        Operation.Update,
                        Operation.Remove
                    }
                }
            });
            RoleAccessUnits.Add(new RoleAccessUnit()
            {
                Role = 1,
                AccessOperation = new AccessOperation()
                {
                    Section = Section.Accounts,
                    Operations = new List<Operation>()
                    {
                        Operation.Read
                    }
                }
            });
            RoleAccessUnits.Add(new RoleAccessUnit()
            {
                Role = 1,
                AccessOperation = new AccessOperation()
                {
                    Section = Section.Roles,
                    Operations = new List<Operation>()
                    {
                        Operation.Read
                    }
                }
            });
        }
    }
}