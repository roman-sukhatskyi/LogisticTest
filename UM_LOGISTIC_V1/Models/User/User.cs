using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using UM_LOGISTIC_V1.Models.Base;

namespace UM_LOGISTIC_V1.Models.User
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string UserPassword { get; set;}
        public virtual Account.Account Account { get; set; }
        [ForeignKey("Account")]
        public long AccountId { get; set; }
        public virtual Role.Role Role { get; set; }
        [ForeignKey("Role")]
        public long RoleId { get; set; }
        public bool Connected { get; set; }
        public string ConnectionId { get; set; }
    }
}