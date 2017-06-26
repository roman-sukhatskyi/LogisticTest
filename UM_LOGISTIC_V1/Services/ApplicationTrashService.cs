using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UM_LOGISTIC_V1.Models;
using UM_LOGISTIC_V1.Models.ApplicationTrash;
using UM_LOGISTIC_V1.Response.ApplicationTrash;

namespace UM_LOGISTIC_V1.Services
{
    public class ApplicationTrashService
    {
        private DataBaseContext db = new DataBaseContext();

        public bool InsertTrashApplication(long userId, long applicationId, bool type)
        {
            var applicationTrash = new ApplicationTrash();
            applicationTrash.CreatedOn = DateTime.Now;
            applicationTrash.ModifiedOn = DateTime.Now;
            applicationTrash.CreatedBy = userId;
            applicationTrash.UserId = userId;
            switch(type)
            {
                case true:
                    applicationTrash.TransportationApplicationId = applicationId;
                    break;
                case false:
                    applicationTrash.CooperationApplicationId = applicationId;
                    break;
            }
            db.ApplicationsTrash.Add(applicationTrash);
            try
            {
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool RemoveTrashElement(long id, bool type)
        {
            switch (type)
            {
                case true:
                    var trashTransportation = db.ApplicationsTrash.Where(x => x.TransportationApplicationId == id).FirstOrDefault();
                    if(trashTransportation != null)
                    {
                        db.ApplicationsTrash.Remove(trashTransportation);
                    }
                    break;
                case false:
                    var trashCooperation = db.ApplicationsTrash.Where(x => x.CooperationApplicationId == id).FirstOrDefault();
                    if (trashCooperation != null)
                    {
                        db.ApplicationsTrash.Remove(trashCooperation);
                    }
                    break;
            }
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public long GetApplicationTrashCountByCreatedBy(long id)
        {
            return db.ApplicationsTrash.Count(i => i.CreatedBy == id);
        }

        public List<ApplicationTrashElement> GetApplicationTrashesList(long userId)
        {
            var applicationsTrash = new List<ApplicationTrashElement>();

            var applications = db.ApplicationsTrash.Select(a => new
            {
                CooperationId = a.CooperationApplicationId,
                TransportationId = a.TransportationApplicationId,
                CooperationTitle = a.CooperationApplication.FullName,
                TransportationTitle = a.TransportationApplication.Name,
                UserId = a.CreatedBy,
            }).Where(a => a.UserId == userId);

            foreach (var app in applications)
            {
                applicationsTrash.Add(new ApplicationTrashElement() {
                    Id = app.CooperationId != null ? app.CooperationId : app.TransportationId,
                    Type = app.CooperationId != null ? false : true,
                    Title = app.CooperationId != null ? app.CooperationTitle : app.TransportationTitle
                });
            }
            return applicationsTrash;
        }
    }
}