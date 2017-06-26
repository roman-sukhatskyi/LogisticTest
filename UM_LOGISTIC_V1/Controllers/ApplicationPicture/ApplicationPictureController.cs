using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UM_LOGISTIC_V1.Services;
using UM_LOGISTIC_V1.Request.ApplicationPicture;
using UM_LOGISTIC_V1.Models.TransportationPicture;
using UM_LOGISTIC_V1.Models.CooperationPicture;
using UM_LOGISTIC_V1.Response.ApplicationPicture;
using System.Web;

namespace UM_LOGISTIC_V1.Controllers.ApplicationPicture
{
    public class ApplicationPictureController : ApiController
    {
        private ApplicationPictureService service = new ApplicationPictureService();

        [Route("api/picture")]
        [HttpGet]
        public IHttpActionResult GetPicture(long applicationId, bool type)
        {
			var response = new GetApplicationPictureResponse();
            var picture = service.GetPicture(applicationId, type);
			response.Success = picture != null ? true : false;
			response.Result = picture != null ? picture : String.Empty;
			return Ok(response);
        }

        [Route("api/pictures")]
        [HttpGet]
        public IHttpActionResult GetPictures(long applicationId, bool type)
        {
            var response = new GetApplicationPicturesResponse();
            response.Success = true;
            var executeUri = type == true ? "/api/t_pictures?id=" : "/api/c_pictures?id=";
            var currentRequestUri = HttpContext.Current.Request;
            var profileUri = currentRequestUri.Url.Scheme + System.Uri.SchemeDelimiter + currentRequestUri.Url.Host + (currentRequestUri.Url.IsDefaultPort ? "" : ":" + currentRequestUri.Url.Port);
            var pictures = service.GetPictures(applicationId, type);
            var html = String.Empty;
            var url = String.Empty;
            var urls = new List<string>();
            foreach (var picture in pictures)
            {
                url = profileUri + executeUri + picture;
                urls.Add(url);
            }
            response.Result = urls;
            return Ok(response);
        }

        [Route("api/picture/load")]
        [HttpPost]
        public IHttpActionResult LoadPicture([FromBody]LoadApplicationPictureRequest request)
        {
			var response = new LoadApplicationPictureResponse();
            var isLoaded = service.LoadPicture(request.Image, request.ApplicationId, request.Type);
			response.Success  = isLoaded;
			return Ok(response);
        }
		
		[Route("api/picture/update")]
        [HttpPost]
        public IHttpActionResult UpdatePicture([FromBody]LoadApplicationPictureRequest request)
        {
			var response = new LoadApplicationPictureResponse();
            var isUpdated = service.UpdatePicture(request.Image, request.ApplicationId, request.Type);
			response.Success  = isUpdated;
			return Ok(response);
        }

        [Route("api/picture/delete")]
        [HttpGet]
        public IHttpActionResult RemovePicture(long applicationId, bool type)
        {
			var response = new DeleteApplicationPictureResponse();
            var isRemoved = service.RemovePicture(applicationId, type);
			response.Success = isRemoved;
			return Ok(response);
        }
    }
}