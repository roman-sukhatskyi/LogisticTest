
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.Request.ApplicationPicture
{
    public class LoadApplicationPictureRequest
    {
        public long ApplicationId { get; set; }
		public bool Type { get; set; }
		public string Image { get; set; }
    }
}