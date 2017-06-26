using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UM_LOGISTIC_V1.Response.ApplicationTrash
{
    public class GetApplicationTrashListResponse : BaseResponse
    {
        public List<ApplicationTrashElement> Result { get; set; }
    }
    public class ApplicationTrashElement
    {
        public long? Id { get; set; }
        public bool Type { get; set; }
        public string Title { get; set; }
    }
}