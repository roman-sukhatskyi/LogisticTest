using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using UM_LOGISTIC_V1.Request.ClientTask;
using UM_LOGISTIC_V1.Response;
using UM_LOGISTIC_V1.Response.ClientTask;
using UM_LOGISTIC_V1.Services;
using UM_LOGISTIC_V1.ApiModels.Filter;
using Microsoft.AspNet.SignalR;
using UM_LOGISTIC_V1.WebHooks;

namespace UM_LOGISTIC_V1.Controllers.ClientTask
{
    public class ClientTaskController : ApiController
    {
        private ClientTaskService service = new ClientTaskService();

        [Route("api/tasks/call_feedback")]
        [HttpPost]
        public IHttpActionResult CreateCallFeedback([FromBody]CallFeedbackRequest request)
        {
            var response = new BaseResponse();
            var isCreated = service.CreateCallFeedback(request);
            response.Success = isCreated;
            return Ok(response);
        }

        [Route("api/tasks")]
        [HttpGet]
        public IHttpActionResult GetClientTasksByPageAndCount(string filter, int page, int count)
        {
			filter = WebUtility.UrlDecode(filter);
            var response = new GetClientTasksResponse();
            var tasks = new List<UM_LOGISTIC_V1.Models.ClientTask.ClientTask>();
            if (filter == String.Empty)
            {
                response.Success = false;
                response.Error = "The filter is empty";
                response.Result = tasks;
                return Ok(response);
            }
            var list = Parser.GetFilterList(filter);
            tasks = service.GetClientTasks(list, page, count);
            response.Success = true;
            response.Error = null;
            response.Result = tasks;
            return Ok(response);
        }

        [Route("api/tasks/count")]
        [HttpGet]
        public IHttpActionResult GetClientTasksCount(string filter)
        {
			filter = WebUtility.UrlDecode(filter);
            var response = new GetClientTasksCountResponse();
            if (filter == String.Empty)
            {
                response.Success = false;
                response.Error = "The filter is empty";
                response.Result = 0;
                return Ok(response);
            }
            response.Success = true;
			var list = Parser.GetFilterList(filter);
            response.Result = service.GetClientTasksCount(list);
            return Ok(response);
        }
        [Route("api/tasks/accept")]
        [HttpPost]
        public IHttpActionResult AcceptTask([FromBody] AcceptTaskRequest request)
        {
            var response = new BaseResponse();
            response.Success = service.AcceptTask(request.Id);
            return Ok(response);
        }

        [Route("api/tasks/app_task")]
        [HttpPost]
        public IHttpActionResult CreateApplicationTask([FromBody]ApplicationTaskRequest request)
        {
            var response = new BaseResponse();
            var isCreated = service.CreateApplicationTask(request);
            response.Success = isCreated;
            return Ok(response);
        }

    }
}