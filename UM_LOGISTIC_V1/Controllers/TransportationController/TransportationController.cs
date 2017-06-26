using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using UM_LOGISTIC_V1.ApiModels.Filter;
using UM_LOGISTIC_V1.Models.TransportationApplication;
using UM_LOGISTIC_V1.Request.TransportationApplication;
using UM_LOGISTIC_V1.Response.TransportationApplication;
using UM_LOGISTIC_V1.Services;

namespace UM_LOGISTIC_V1.Controllers.TransportationController
{
    public class TransportationController : ApiController
    {
        private TransportationApplicationService applicationService = new TransportationApplicationService();
        private ApplicationPictureService pictureService = new ApplicationPictureService();

        [Route("api/transportation")]
        [HttpGet]
        public IHttpActionResult GetTransportationApplication(long id, string token, string user)
        {
            var getTransportationApplicationResponse = new GetTransportationApplicationResponse();
            var applicationInfo = applicationService.GetTransportationApplication(id);
            if (user != null)
            {
                getTransportationApplicationResponse.Success = true;
                getTransportationApplicationResponse.Error = "";
                getTransportationApplicationResponse.Result = applicationInfo;
                return Ok(getTransportationApplicationResponse);
            }
            else
            {
                getTransportationApplicationResponse.Success = false;
                getTransportationApplicationResponse.Error = "";
                getTransportationApplicationResponse.Result = null;
                return Ok(getTransportationApplicationResponse);
            }
        }

        [Route("api/transportation/create")]
        [HttpPost]
        public IHttpActionResult CreateTransportationApplication([FromBody]CreateTransportationApplicationRequest request)
        {
            var createTransportationApplicationResponse = new CreateTransportationApplicationResponse();
            var applicationToCreate = new UM_LOGISTIC_V1.Models.TransportationApplication.TransportationApplication()
            {
                Name = request.Name,
                ContactPhone = request.ContactPhone,
                SendAddress = request.SendAddress,
                DeliveryAddress = request.DeliveryAddress,
                CompleteDate = request.CompleteDate,
                ShipmentType = request.ShipmentType,
                ShipmentCapacity = request.ShipmentCapacity,
                ShipmentHeight = request.ShipmentHeight,
                ShipmentLength = request.ShipmentLength,
                ShipmentWeight = request.ShipmentWeight,
                ShipmentWidth = request.ShipmentWidth,
                CreatedBy = request.CreatedBy
            };
            var id = applicationService.CreateTransportationApplication(applicationToCreate, request.Image);
            if (id != null)
            {
                createTransportationApplicationResponse.Success = true;
                createTransportationApplicationResponse.Error = "";
                createTransportationApplicationResponse.Id = id;
                return Ok(createTransportationApplicationResponse);
            }
            else
            {
                createTransportationApplicationResponse.Success = false;
                createTransportationApplicationResponse.Error = "";
                createTransportationApplicationResponse.Id = null;
                return Ok(createTransportationApplicationResponse);
            }
        }

        [Route("api/transportation/update")]
        [HttpPost]
        public IHttpActionResult UpdateTransportationApplication([FromBody]UpdateTransportationApplicationRequest request)
        {
            var updateTransportationApplicationResponse = new UpdateTransportationApplicationResponse();
            var applicationToUpdate = new UM_LOGISTIC_V1.Models.TransportationApplication.TransportationApplication()
            {
                Id = request.Id,
                Name = request.Name,
                ContactPhone = request.ContactPhone,
                SendAddress = request.SendAddress,
                DeliveryAddress = request.DeliveryAddress,
                CompleteDate = request.CompleteDate,
                ShipmentType = request.ShipmentType,
                ShipmentCapacity = request.ShipmentCapacity,
                ShipmentHeight = request.ShipmentHeight,
                ShipmentLength = request.ShipmentLength,
                ShipmentWeight = request.ShipmentWeight,
                ShipmentWidth = request.ShipmentWidth
            };
            var isUpdate = applicationService.UpdateTransportationApplication(applicationToUpdate);
            if (isUpdate)
            {
                updateTransportationApplicationResponse.Success = true;
                updateTransportationApplicationResponse.Error = "";
                updateTransportationApplicationResponse.Result = null;
                return Ok(updateTransportationApplicationResponse);
            }
            else
            {
                updateTransportationApplicationResponse.Success = false;
                updateTransportationApplicationResponse.Error = "";
                updateTransportationApplicationResponse.Result = null;
                return Ok(updateTransportationApplicationResponse);
            }
        }

        [Route("api/transportation/delete")]
        [HttpPost]
        public IHttpActionResult RemoveTransportationApplication([FromBody]RemoveTransportationApplicationRequest request)
        {
            var deleteTransportationApplicationResponse = new DeleteTransportationApplicationResponse();
            var applicationIdToDelete = request.Id;
            var isDeleted = applicationService.RemoveTransportationApplication(applicationIdToDelete);
            if (isDeleted)
            {
                deleteTransportationApplicationResponse.Success = true;
                deleteTransportationApplicationResponse.Error = "";
                deleteTransportationApplicationResponse.Result = null;
                return Ok(deleteTransportationApplicationResponse);
            }
            else
            {
                deleteTransportationApplicationResponse.Success = false;
                deleteTransportationApplicationResponse.Error = "";
                deleteTransportationApplicationResponse.Result = null;
                return Ok(deleteTransportationApplicationResponse);
            }
        }

        [Route("api/transportations")]
        [HttpGet]
        public IHttpActionResult GetTransportationApplicationsByPageAndCount(int page, int count, string token, string user)
        {
            var getTransportationApplicationsByPageAndCountResponse = new GetTransportationApplicationsByPageAndCountResponse();
            var applications = applicationService.GetTransportationApplications(page, count);
            if (applications != null)
            {
                getTransportationApplicationsByPageAndCountResponse.Success = true;
                getTransportationApplicationsByPageAndCountResponse.Error = "";
                getTransportationApplicationsByPageAndCountResponse.Result = applications;
                return Ok(getTransportationApplicationsByPageAndCountResponse);
            }
            else
            {
                getTransportationApplicationsByPageAndCountResponse.Success = false;
                getTransportationApplicationsByPageAndCountResponse.Error = "";
                getTransportationApplicationsByPageAndCountResponse.Result = null;
                return Ok(getTransportationApplicationsByPageAndCountResponse);
            }
        }

        [Route("api/trans_not_filtered")]
        [HttpGet]
        public IHttpActionResult GetNotFilteredTransportationApplicationsByPageAndCount(int page, int count, string token, string user)
        {
            var getTransportationApplicationsByPageAndCountResponse = new GetTransportationApplicationsByPageAndCountResponse();
            var applications = applicationService.GetNotFilteredTransportationApplications(page, count);
            if (applications != null)
            {
                getTransportationApplicationsByPageAndCountResponse.Success = true;
                getTransportationApplicationsByPageAndCountResponse.Error = "";
                getTransportationApplicationsByPageAndCountResponse.Result = applications;
                return Ok(getTransportationApplicationsByPageAndCountResponse);
            }
            else
            {
                getTransportationApplicationsByPageAndCountResponse.Success = false;
                getTransportationApplicationsByPageAndCountResponse.Error = "";
                getTransportationApplicationsByPageAndCountResponse.Result = null;
                return Ok(getTransportationApplicationsByPageAndCountResponse);
            }
        }

        [Route("api/t_applications")]
        [HttpGet]
        public IHttpActionResult GetApplications(string filter, int page, int count)
        {
            filter = WebUtility.UrlDecode(filter);
            var response = new GetTransportationApplicationsByPageAndCountResponse();
            var applications = new List<TransportationApplication>();
            var list = Parser.GetFilterList(filter);
            applications = applicationService.GetApplications(list, page, count);
            response.Success = true;
            response.Error = null;
            response.Result = applications;
            return Ok(response);
        }

        [Route("api/t_pictures")]
        [HttpGet]
        public HttpResponseMessage GetPicture(long id)
        {
            var image = applicationService.GetPicture(id);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            if (image == null)
            {
                return response;
            }
            MemoryStream ms = new MemoryStream(image);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
            return response;
        }

        [Route("api/get_list_pictures")]
        [HttpGet]
        public IHttpActionResult GetListHtmlPictures(long id, bool type, bool isEdit = false)
        {
            var executeUri = type == true ? "/api/t_pictures?id=" : "/api/c_pictures?id=";
            var currentRequestUri = HttpContext.Current.Request;
            var profileUri = currentRequestUri.Url.Scheme + System.Uri.SchemeDelimiter + currentRequestUri.Url.Host + (currentRequestUri.Url.IsDefaultPort ? "" : ":" + currentRequestUri.Url.Port);
            var pictures = pictureService.GetPictures(id, type);
            var url = String.Empty;
            var htmlHrefs = new List<string>();
            var index = 1;
            foreach(var picture in pictures)
            {
                url = profileUri + executeUri + picture;
                htmlHrefs.Add("<img style='width: 250px; height: 200px;' class='jslghtbx-thmb' src='" + url + "' alt='' data-jslghtbx='" + url + "' data-jslghtbx-group='mygroup1'>" + (isEdit == true ? "<button id='EditPicture" + index + "' class='btn btn-warning btn-sm'>Видалити</button>" : ""));
                index++;
            }
            return Ok(htmlHrefs);
        }
    }
}