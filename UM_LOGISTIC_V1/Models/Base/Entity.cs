using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.Models.Base
{
    public class Entity
    {
        public long Id { get; set; }
        public DateTime CreatedOn { get; set; }
        [Index("ModifiedOnIndex")]
        public DateTime ModifiedOn { get; set; }
        public long CreatedBy { get; set; }
    }
}