using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using UM_LOGISTIC_V1.ApiModels.Filter;
using UM_LOGISTIC_V1.Models;
using UM_LOGISTIC_V1.Models.CooperationApplication;

namespace UM_LOGISTIC_V1.Services
{
    public class CooperationApplicationService
    {
        private DataBaseContext db = new DataBaseContext();

        public CooperationApplication GetCooperationApplication(long id)
        {
            var application = db.CooperationApplications.Find(id);
            return application;
        }

        public long? CreateCooperationApplication(CooperationApplication application, string image)
        {
            if (application != null)
            {
                application.CreatedOn = DateTime.Now;
                application.ModifiedOn = application.CreatedOn;
                if (!String.IsNullOrEmpty(image))
                {
                    application.Pictures.Add(new UM_LOGISTIC_V1.Models.CooperationPicture.CooperationPicture()
                    {
                        Image = image,
                        CreatedOn = DateTime.Now,
                        ModifiedOn = DateTime.Now,
                        CreatedBy = application.CreatedBy
                    });
                }
                db.CooperationApplications.Add(application);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return null;
                }
                return application.Id;
            }
            return null;
        }

        public bool UpdateCooperationApplication(CooperationApplication application)
        {
            var applicationToUpdate = db.CooperationApplications.Find(application.Id);
            if (applicationToUpdate != null)
            {
                applicationToUpdate.FullName = application.FullName;
                applicationToUpdate.ResidenceAddress = application.ResidenceAddress;
                applicationToUpdate.ParkingPlace = application.ParkingPlace;
                applicationToUpdate.ContactPhone = application.ContactPhone;
                applicationToUpdate.IsPhysicalPerson = application.IsPhysicalPerson;
                applicationToUpdate.IsBussinessPerson = application.IsBussinessPerson;
                applicationToUpdate.CarModel = application.CarModel;
                applicationToUpdate.TransportLength = application.TransportLength;
                applicationToUpdate.TransportWidth = application.TransportWidth;
                applicationToUpdate.TransportHeight = application.TransportHeight;
                applicationToUpdate.TransportWeight = application.TransportWeight;
                applicationToUpdate.TransportCapacity = application.TransportCapacity;
                applicationToUpdate.TransportArrow = application.TransportArrow;
                applicationToUpdate.WorkCost = application.WorkCost;
                applicationToUpdate.WorkTypeId = application.WorkTypeId;
                applicationToUpdate.DeliveryCost = application.DeliveryCost;
                applicationToUpdate.ModifiedOn = DateTime.Now;
                db.Entry(applicationToUpdate).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public bool RemoveCooperationApplication(long id)
        {
            var applicationToDelete = db.CooperationApplications.Find(id);
            if (applicationToDelete != null)
            {
                db.CooperationApplications.Remove(applicationToDelete);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public List<CooperationApplication> GetCooperationApplications(int page, int count)
        {
            var applications = from u in db.CooperationApplications
                               where u.Filtered == true
                        orderby u.ModifiedOn descending
                        select u;
            if (applications == null)
            {
                return null;
            }
            var limitedApplications = applications.Skip(count * page).Take(count).ToList();
            return limitedApplications;
        }

        public List<CooperationApplication> GetNotFilteredCooperationApplications(int page, int count)
        {
            var applications = from u in db.CooperationApplications
                               where u.Filtered == false
                               orderby u.ModifiedOn descending
                               select u;
            if (applications == null)
            {
                return null;
            }
            var limitedApplications = applications.Skip(count * page).Take(count).ToList();
            return limitedApplications;
        }

        public List<ApplicationWorkType> GetApplicationWorkTypes()
        {
            return db.ApplicationWorkTypes.ToList();

        }

        public List<CooperationApplication> GetApplications(List<Filter> filters, int page, int count)
        {
            var columns = DBColumns.cooperationAplicationcolumns;
            var query = db.CooperationApplications.AsQueryable<CooperationApplication>();
            if (filters.Count == 0)
            {
                return query.ToList().Skip(count * page).Take(count).ToList();
            }
            foreach (var filter in filters)
            {
                object value = Convert.ChangeType(filter.value, columns.Where(s => s.column == filter.column).Select(x => x.type).FirstOrDefault());
                var comparer = new OperatorComparer();
                var isOperation = AvaibleOperations.operations.TryGetValue(filter.operation, out comparer);
                var predicate = ExpressionBuilder.BuildPredicate<CooperationApplication>(value, comparer, filter.column);
                query = query.Where(predicate);
            }
            query = query.OrderByDescending(i => i.ModifiedOn);
            return query.ToList().Skip(count * page).Take(count).ToList();
        }

        public byte[] GetPicture(long id)
        {
            var image = db.CooperationPictures.Find(id);
            if (image == null)
            {
                return null;
            }
            var base64Data = Regex.Match(image.Image, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            var binData = Convert.FromBase64String(base64Data);
            return binData;
        }
    }
}