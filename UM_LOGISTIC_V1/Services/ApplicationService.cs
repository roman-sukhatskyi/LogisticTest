using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UM_LOGISTIC_V1.Models;
using UM_LOGISTIC_V1.Models.TransportationApplication;

namespace UM_LOGISTIC_V1.Services
{
    public class ApplicationService
    {
        private DataBaseContext db = new DataBaseContext();

        public long AcceptApplication(bool type, long id)
        {
            switch(type)
            {
                case true:
                    var transportationApplication = db.TransportationApplications.Find(id);
                    if(transportationApplication != null)
                    {
                        transportationApplication.Filtered = true;
                        db.Entry(transportationApplication).State = EntityState.Modified;
                        try
                        {
                            db.SaveChanges();
                            return transportationApplication.Id;
                        }
                        catch(Exception)
                        {
                            return 0L;
                        }
                    }
                    break;
                case false:
                    var cooperationApplication = db.CooperationApplications.Find(id);
                    if (cooperationApplication != null)
                    {
                        cooperationApplication.Filtered = true;
                        db.Entry(cooperationApplication).State = EntityState.Modified;
                        try
                        {
                            db.SaveChanges();
                            return cooperationApplication.Id;
                        }
                        catch (Exception)
                        {
                            return 0L;
                        }
                    }
                    break;
            }
            return 0L;
        }

        public bool DeclineApplication(bool type, long id)
        {
            switch (type)
            {
                case true:
                    var transportationApplication = db.TransportationApplications.Find(id);
                    if (transportationApplication != null)
                    {
                        db.TransportationApplications.Remove(transportationApplication);
                        try
                        {
                            db.SaveChanges();
                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                    break;
                case false:
                    var cooperationApplication = db.CooperationApplications.Find(id);
                    if (cooperationApplication != null)
                    {
                        db.CooperationApplications.Remove(cooperationApplication);
                        try
                        {
                            db.SaveChanges();
                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                    break;
            }
            return false;
        }

        public long GetNotFilteredApplicationsCount()
        {
            var transportationsCount = db.TransportationApplications.Count(t => t.Filtered == false);
            var cooperationsCount = db.CooperationApplications.Count(t => t.Filtered == false);
            return transportationsCount + cooperationsCount;
        }

        public List<object> GetOrderedByMeApplications(bool type, long userId, int page, int count)
        {
            switch(type)
            {
                case true:
                    var t_applicationIds = db.ClientTasks.Where(t => t.UserId == userId && t.TransportationApplicationId != null).Select(i => i.TransportationApplicationId);
                    var t_myApplications = (from t in db.TransportationApplications
                                  where t_applicationIds.Contains(t.Id)
                    select t).ToList<object>().Skip(count * page).Take(count).ToList(); ;
                    return t_myApplications;
                case false:
                    var c_applciationIds = db.ClientTasks.Where(t => t.UserId == userId && t.CooperationApplicationId != null).Select(i => i.CooperationApplicationId);
                    var c_myApplications = (from t in db.CooperationApplications
                                          where c_applciationIds.Contains(t.Id)
                                          select t).ToList<object>().Skip(count * page).Take(count).ToList(); ;
                    return c_myApplications;
            }
            return new List<object>();
        }

        public bool UpToDateApplication(bool type, long id)
        {
            switch (type)
            {
                case true:
                    var t_application = db.TransportationApplications.Find(id);
                    if(t_application != null)
                    {
                        t_application.ModifiedOn = DateTime.Now;
                        db.Entry(t_application).State = EntityState.Modified;
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
                case false:
                    var c_application = db.CooperationApplications.Find(id);
                    if (c_application != null)
                    {
                        c_application.ModifiedOn = DateTime.Now;
                        db.Entry(c_application).State = EntityState.Modified;
                        try
                        {
                            db.SaveChanges();
                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                    return false;
            }
            return false;

        }

        public bool RemoveApplication(bool type, long id)
        {
            switch (type)
            {
                case true:
                    var t_application = db.TransportationApplications.Find(id);
                    if (t_application != null)
                    {
                        db.TransportationApplications.Remove(t_application);
                        try
                        {
                            db.SaveChanges();
                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                    return false;
                case false:
                    var c_application = db.CooperationApplications.Find(id);
                    if (c_application != null)
                    {
                        db.CooperationApplications.Remove(c_application);
                        try
                        {
                            db.SaveChanges();
                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                    return false;
            }
            return false;

        }
    }
}