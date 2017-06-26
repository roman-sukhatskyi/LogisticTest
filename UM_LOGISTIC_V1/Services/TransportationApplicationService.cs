using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using UM_LOGISTIC_V1.ApiModels.Filter;
using UM_LOGISTIC_V1.Models;
using UM_LOGISTIC_V1.Models.TransportationApplication;

namespace UM_LOGISTIC_V1.Services
{
    public class TransportationApplicationService
    {
        private DataBaseContext db = new DataBaseContext();

        public TransportationApplication GetTransportationApplication(long id)
        {
            var application = db.TransportationApplications.Find(id);
            return application;
        }

        public long? CreateTransportationApplication(TransportationApplication application, string image)
        {
            if (application != null)
            {
                application.CreatedOn = DateTime.Now;
                application.ModifiedOn = application.CreatedOn;
                if(!String.IsNullOrEmpty(image))
                {
                    application.Pictures.Add(new UM_LOGISTIC_V1.Models.TransportationPicture.TransportationPicture()
                    {
                        Image = image,
                        CreatedOn = DateTime.Now,
                        ModifiedOn = DateTime.Now,
                        CreatedBy = application.CreatedBy
                    });
                }
                db.TransportationApplications.Add(application);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return null;
                }
                return application.Id;
            }
            return null;
        }

        public bool UpdateTransportationApplication(TransportationApplication application)
        {
            var applicationToUpdate = db.TransportationApplications.Find(application.Id);
            if (applicationToUpdate != null)
            {
                applicationToUpdate.Name = application.Name;
                applicationToUpdate.ContactPhone = application.ContactPhone;
                applicationToUpdate.SendAddress = application.SendAddress;
                applicationToUpdate.DeliveryAddress = application.DeliveryAddress;
                applicationToUpdate.CompleteDate = application.CompleteDate;
                applicationToUpdate.ShipmentType = application.ShipmentType;
                applicationToUpdate.ShipmentLength = application.ShipmentLength;
                applicationToUpdate.ShipmentWidth = application.ShipmentWidth;
                applicationToUpdate.ShipmentHeight = application.ShipmentHeight;
                applicationToUpdate.ShipmentCapacity = application.ShipmentCapacity;
                applicationToUpdate.ShipmentWeight = application.ShipmentWeight;
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

        public bool RemoveTransportationApplication(long id)
        {
            var applicationToDelete = db.TransportationApplications.Find(id);
            if (applicationToDelete != null)
            {
                db.TransportationApplications.Remove(applicationToDelete);
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

        public List<TransportationApplication> GetTransportationApplications(int page, int count)
        {
            var applications = from u in db.TransportationApplications
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

        public List<TransportationApplication> GetNotFilteredTransportationApplications(int page, int count)
        {
            var applications = from u in db.TransportationApplications
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

        public List<TransportationApplication> GetApplications(List<Filter> filters, int page, int count)
        {
            var columns = DBColumns.transportationAplicationcolumns;
            var query = db.TransportationApplications.AsQueryable<TransportationApplication>();
            if(filters.Count == 0)
            {
                return query.ToList().Skip(count * page).Take(count).ToList();
            }
            foreach (var filter in filters)
            {
                object value = Convert.ChangeType(filter.value, columns.Where(s => s.column == filter.column).Select(x => x.type).FirstOrDefault());
                var comparer = new OperatorComparer();
                var isOperation = AvaibleOperations.operations.TryGetValue(filter.operation, out comparer);
                var predicate = ExpressionBuilder.BuildPredicate<TransportationApplication>(value, comparer, filter.column);
                query = query.Where(predicate);
            }
            query = query.OrderByDescending(i => i.ModifiedOn);
            return query.ToList().Skip(count * page).Take(count).ToList();
        }

        public byte[] GetPicture(long id)
        {
            var image = db.TransportationPictures.Find(id);
            if(image == null)
            {
                return null;
            }
            var base64Data = Regex.Match(image.Image, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            var binData = Convert.FromBase64String(base64Data);
            return binData;
        }
    }
}