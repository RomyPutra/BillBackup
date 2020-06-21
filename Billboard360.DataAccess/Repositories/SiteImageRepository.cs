using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;

namespace Billboard360.DataAccess.Repositories
{
    public class SiteImageRepository
    {
        DatabaseContext db = new DatabaseContext();
        public SiteImageRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public IQueryable<TitikLokasiPrice> GetBySiteID(Guid siteID)
        {
            return db.TitikLokasiPrice.Where(x => x.ID == siteID);
        }

        public Response Insert(List<TitikLokasiImage> data, bool isCommit = true)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                //db.Add(data);
                db.Set<TitikLokasiImage>().AddRange(data);

                if (isCommit)
                {
                    db.SaveChanges();

                    message = "Save data Image success";
                    result = true;
                }

                res.ID = data.FirstOrDefault().TitikLokasiID;
                res.Message = message;
                res.Result = result;

                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Result = false;

                return res;
            }
        }

        public Response Update(Guid siteID, List<TitikLokasiImage> data, bool isCommit = true)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                db.TitikLokasiImage.RemoveRange(db.TitikLokasiImage.Where(x => x.TitikLokasiID == siteID));

                db.Set<TitikLokasiImage>().AddRange(data);

                if (isCommit)
                {
                    db.SaveChanges();

                    message = "Update data success";
                    result = true;
                }

                res.ID = data.FirstOrDefault().TitikLokasiID;
                res.Message = message;
                res.Result = result;

                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Result = false;

                return res;
            }
        }
    }
}
