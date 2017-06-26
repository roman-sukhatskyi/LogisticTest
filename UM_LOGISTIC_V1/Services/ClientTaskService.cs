using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UM_LOGISTIC_V1.Models;
using UM_LOGISTIC_V1.Models.ClientTask;
using UM_LOGISTIC_V1.Request.ClientTask;
using UM_LOGISTIC_V1.ApiModels.Filter;
using Microsoft.AspNet.SignalR;
using UM_LOGISTIC_V1.WebHooks;

namespace UM_LOGISTIC_V1.Services
{
    public class ClientTaskService
    {
        private DataBaseContext db = new DataBaseContext();

        public bool CreateCallFeedback(CallFeedbackRequest request)
        {
            var feedBack = new ClientTask();
            var minOwner = db.ClientTasks.
            GroupBy(item => new { OwnerId = item.Owner.Id, TypeId = item.Owner.RoleId }).
            Select(group => new {
                OwnerId = group.Key.OwnerId,
                Count = group.Count(),
                RoleId = group.Key.TypeId
            }).
            Where(t => t.RoleId == 2).
            OrderBy(item => item.Count).FirstOrDefault();
            var owner = 0L;
            if (minOwner == null)
            {
                var firstOwner = db.Users.Where(u => u.RoleId == 2).FirstOrDefault();
                if (firstOwner != null)
                {
                    owner = firstOwner.Id;
                }
                else
                {
                    owner = 1;
                }
            }
            else
            {
                owner = minOwner.OwnerId;
            }
            var title = "Телефон: " + request.Phone + "\r\n";
            title += "Ім'я: " + request.Name + "\r\n";
            title += "Запитання: " + request.Question + "\r\n";
            feedBack.Title = title;
            feedBack.TypeId = 1;
            feedBack.CreatedOn = DateTime.Now;
            feedBack.ModifiedOn = DateTime.Now;
			feedBack.OwnerId = owner;
            feedBack.UserId = request.UserId;
            db.ClientTasks.Add(feedBack);
            try
            {
                db.SaveChanges();
                var eventsHub = GlobalHost.ConnectionManager.GetHubContext<EventsHub>();
                eventsHub.Clients.All.onTaskGot(owner, title, 1);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public List<ClientTask> GetClientTasks(int page, int count, long userId)
        {
            var tasks = from t in db.ClientTasks
						where t.OwnerId == userId
                        orderby t.Id ascending
                        select t;
            if (tasks == null)
            {
                return null;
            }
            var limitedTasks = tasks.Skip(count * page).Take(count).ToList();
            return limitedTasks;
        }

        public long GetClientTasksCount(List<Filter> filters)
        {
			var columns = DBColumns.clientTaskcolumns;
            var query = db.ClientTasks.AsQueryable<ClientTask>();
            if (filters.Count == 0)
            {
                return 0;
            }
            foreach (var filter in filters)
            {
                object value = Convert.ChangeType(filter.value, columns.Where(s => s.column == filter.column).Select(x => x.type).FirstOrDefault());
                var comparer = new OperatorComparer();
                var isOperation = AvaibleOperations.operations.TryGetValue(filter.operation, out comparer);
                var predicate = ExpressionBuilder.BuildPredicate<ClientTask>(value, comparer, filter.column);
                query = query.Where(predicate);
            }
            return query.ToList().Count();
        }

        public bool AcceptTask(long id)
        {
            var task = db.ClientTasks.Find(id);
            if(task != null)
            {
                db.ClientTasks.Remove(task);
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch(Exception)
                {
                    return false;
                }
            }
            return false;
        }
		
		public List<ClientTask> GetClientTasks(List<Filter> filters, int page, int count)
        {
            var columns = DBColumns.clientTaskcolumns;
            var query = db.ClientTasks.AsQueryable<ClientTask>();
            if (filters.Count == 0)
            {
                return new List<ClientTask>();
            }
            foreach (var filter in filters)
            {
                object value = Convert.ChangeType(filter.value, columns.Where(s => s.column == filter.column).Select(x => x.type).FirstOrDefault());
                var comparer = new OperatorComparer();
                var isOperation = AvaibleOperations.operations.TryGetValue(filter.operation, out comparer);
                var predicate = ExpressionBuilder.BuildPredicate<ClientTask>(value, comparer, filter.column);
                query = query.Where(predicate);
            }
            return query.ToList().Skip(count * page).Take(count).ToList();
        }

        public bool CreateApplicationTask(ApplicationTaskRequest request)
        {
            var applicationTask = new ClientTask();
            var minOwner = db.ClientTasks.
            GroupBy(item => new { OwnerId = item.Owner.Id, TypeId = item.Owner.RoleId }).
            Select(group => new {
                OwnerId = group.Key.OwnerId,
                Count = group.Count(),
                RoleId = group.Key.TypeId
            }).
            Where(t => t.RoleId == 2).
            OrderBy(item => item.Count).FirstOrDefault();
            var owner = 0L;
            if(minOwner == null)
            {
                var firstOwner = db.Users.Where(u => u.RoleId == 2).FirstOrDefault();
                if (firstOwner != null)
                {
                    owner = firstOwner.Id;
                }
                else
                {
                    owner = 1;
                }
            }
            else
            {
                owner = minOwner.OwnerId;
            }
            var title = String.Empty;
            applicationTask.TypeId = request.TypeId;
            applicationTask.CreatedOn = DateTime.Now;
            applicationTask.ModifiedOn = DateTime.Now;
            applicationTask.OwnerId = owner;
            applicationTask.UserId = request.UserId;
            switch(request.TypeId)
            {
                case 2:
                    applicationTask.TransportationApplicationId = request.ApplicationId;
                    title = "Оформлено заявку на перевезення";
                    break;
                case 3:
                    applicationTask.CooperationApplicationId = request.ApplicationId;
                    title = "Оформлено заявку на співробітництво";
                    break;
            }
            applicationTask.Title = title;
            db.ClientTasks.Add(applicationTask);
            try
            {
                db.SaveChanges();
                var eventsHub = GlobalHost.ConnectionManager.GetHubContext<EventsHub>();
                eventsHub.Clients.All.onTaskGot(owner, title, request.TypeId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}