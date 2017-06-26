using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UM_LOGISTIC_V1.Models;
using UM_LOGISTIC_V1.Models.CooperationPicture;
using UM_LOGISTIC_V1.Models.TransportationPicture;

namespace UM_LOGISTIC_V1.Services
{
    public class ApplicationPictureService
    {
        private DataBaseContext db = new DataBaseContext();

        public string GetPicture(long id, bool type)
        {
            switch (type)
            {
                case false:
                    var picture = db.CooperationPictures.Where(x => x.CooperationApplicationId == id).FirstOrDefault();
                    var data = picture != null ? picture.Image : null;
                    return data;
                case true:
                    var picture2 = db.TransportationPictures.Where(x => x.TransportationApplicationId == id).FirstOrDefault();
                    var data2 = picture2 != null ? picture2.Image : null;
                    return data2;
                default:
                    return null;
            }
        }

        public List<long> GetPictures(long id, bool type)
        {
            switch (type)
            {
                case false:
                    var pictures = db.CooperationPictures.Where(x => x.CooperationApplicationId == id).Select(u => u.Id).ToList<long>();
                    return pictures;    
                case true:
                    var pictures2 = db.TransportationPictures.Where(x => x.TransportationApplicationId == id).Select(u => u.Id).ToList<long>();
                    return pictures2;
                default:
                    return null;
            }
        }

        public bool LoadPicture(string image, long applicationId, bool type)
        {
            switch (type)
            {
                case false:
                    var cooperationPicture = new CooperationPicture();
                    cooperationPicture.CooperationApplicationId = applicationId;
                    cooperationPicture.Image = image;
                    cooperationPicture.CreatedOn = DateTime.Now;
                    cooperationPicture.ModifiedOn = cooperationPicture.CreatedOn;
                    db.CooperationPictures.Add(cooperationPicture);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    return true;

                case true:
                    var transportationPicture = new TransportationPicture();
                    transportationPicture.TransportationApplicationId = applicationId;
                    transportationPicture.Image = image;
                    transportationPicture.CreatedOn = DateTime.Now;
                    transportationPicture.ModifiedOn = transportationPicture.CreatedOn;
                    db.TransportationPictures.Add(transportationPicture);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    return true;
                default:
                    return false;
            }
        }
		
		public bool UpdatePicture(string image, long applicationId, bool type)
        {
            switch (type)
            {
                case false:
                    var cooperationPicture = db.CooperationPictures.Find(applicationId);
					if(cooperationPicture != null)
					{
						cooperationPicture.CooperationApplicationId = applicationId;
						cooperationPicture.Image = image;
                        cooperationPicture.ModifiedOn = DateTime.Now;
                        db.Entry(cooperationPicture).State = EntityState.Modified;
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

                case true:
                    var transportationPicture = db.TransportationPictures.Find(applicationId);
					if(transportationPicture != null)
					{
						transportationPicture.TransportationApplicationId = applicationId;
						transportationPicture.Image = image;
                        transportationPicture.ModifiedOn = DateTime.Now;
                        db.Entry(transportationPicture).State = EntityState.Modified;
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
                default:
                    return false;
            }
        }

        public bool RemovePicture(long id, bool type)
        {
            switch (type)
            {
                case false:
                    var pictureToDelete = db.CooperationPictures.Find(id);
                    if (pictureToDelete != null)
                    {
                        db.CooperationPictures.Remove(pictureToDelete);
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

                case true:
                    var pictureToDelete2 = db.TransportationPictures.Find(id);
                    if (pictureToDelete2 != null)
                    {
                        db.TransportationPictures.Remove(pictureToDelete2);
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
                default:
                    return false;
            }
        }
    }
}