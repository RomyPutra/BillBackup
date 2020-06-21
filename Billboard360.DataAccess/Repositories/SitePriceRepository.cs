using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;


namespace Billboard360.DataAccess.Repositories
{
    public class SitePriceRepository
    {
        DatabaseContext db = new DatabaseContext();
        public SitePriceRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }


        public Response Insert(List<TitikLokasiPrice> data, bool isCommit = true)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                db.Set<TitikLokasiPrice>().AddRange(data);

                if (isCommit)
                {
                    db.SaveChanges();

                    message = "Save data Success";
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

        public Response DeleteRangeBySiteID(Guid titikLokasiID, Guid userID, bool isCommit)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                if (titikLokasiID != Guid.Empty)
                {
                    var find = (from ds in db.TitikLokasiPrice
                                where ds.ID == titikLokasiID && ds.DeletedDate == null
                                select ds).ToList();

                    if (find != null)
                    {
                        foreach (var x in find)
                        {
                            x.DeletedByUserID = userID;
                            x.DeletedDate = DateTime.Now;
                        }
                    }

                    message = "Delete success";
                    result = true;
                }

                res.ID = titikLokasiID;
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

        public Response Update(Guid siteID, List<TitikLokasiPrice> data, bool isCommit = true)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {

                db.TitikLokasiPrice.RemoveRange(db.TitikLokasiPrice.Where(x => x.TitikLokasiID == siteID));

                db.Set<TitikLokasiPrice>().AddRange(data);

                if (isCommit)
                {
                    db.SaveChanges();

                    message = "Save data Success";
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

        public IQueryable<TitikLokasiPrice> FindPrice(Guid siteItemID, double price)
        {
            return db.TitikLokasiPrice.Where(x => x.TitikLokasiDetailID == siteItemID && x.HargaAwal == price);
        }
    }
}
