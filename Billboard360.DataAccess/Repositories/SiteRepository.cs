using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;


namespace Billboard360.DataAccess.Repositories
{
    public class SiteRepository
    {
        DatabaseContext db = new DatabaseContext();
        public SiteRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public IQueryable<TitikLokasi> GetAll()
        {
            return db.TitikLokasi.Where(x => x.DeletedDate == null);
        }

        public IQueryable<TitikLokasi> GetSiteByID(Guid id)
        {
            return db.TitikLokasi.Where(x => x.ID == id);
        }

        public IQueryable<TitikLokasi> GetSiteByNoBillboard(string noBillboard)
        {
            return db.TitikLokasi.Where(x => x.NoBillboard == noBillboard);
        }

        public Response UpdateRateScore(LogRateSite data)
        {
            Response res = new Response();
            string message = "Failed Update Rate Score";
            bool result = false;

            var dataRateScore = (from x in db.LogRateSite
                                   where x.SiteID == data.SiteID 
                                   select x.RateScore).ToList();

            if (dataRateScore != null && dataRateScore.Count > 0)
            {
                var rateScoreTotal = dataRateScore.Select(x => x).Sum();
                var rateScoreAverage = rateScoreTotal / dataRateScore.Count;

                var find = db.TitikLokasi.Where(x => x.ID == data.SiteID).FirstOrDefault();

                if(find != null)
                {
                    find.RateScoreAverage = rateScoreAverage;
                    find.RateScoreTotal = rateScoreTotal;

                    db.SaveChanges();

                    message = "Update data success";
                    result = true;
                }
                
            }

            res.ID = data.ID;
            res.Message = message;
            res.Result = result;
            return res;
     
        }

        public Response Insert(TitikLokasi data, bool isCommit = true)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                db.Set<TitikLokasi>().Add(data);

                if(isCommit)
                {
                    db.SaveChanges();

                    message = "Save data bank success";
                    result = true;
                }

                res.ID = data.ID;
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

        public Response Update(TitikLokasi data, bool isCommit = true)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                if (data.ID != Guid.Empty)
                {

                    var temp = (from x in db.TitikLokasi
                                where x.ID == data.ID
                                select x).FirstOrDefault();

                    temp.KelasJalan = data.KelasJalan;
                    temp.AdaKontruksi = data.AdaKontruksi;
                    temp.Address = data.Address;
                    temp.AddressReal = data.AddressReal;
                    temp.AdvTypeID = data.AdvTypeID;
                    temp.Cabang = data.Cabang;
                    temp.KodeFile = data.KodeFile;
                    temp.Kota = data.Kota;
                    temp.Lampu = data.Lampu;
                    temp.LastUpdateByUserID = data.LastUpdateByUserID;
                    temp.LastUpdateDate = DateTime.Now;
                    temp.Latitude = data.Latitude;
                    temp.Longitude = data.Longitude;
                    temp.NoBillboard = data.NoBillboard;
                    temp.PIC = data.PIC;
                    temp.Provinsi = data.Provinsi;
                    temp.Pulau = data.Pulau;
                    temp.Status = data.Status;
                    temp.Tinggi = data.Tinggi;
                    temp.TransaksiCount = data.TransaksiCount;
                    temp.Type = data.Type;
                    temp.LastUpdateByUserID = data.LastUpdateByUserID;
                    temp.LastUpdateDate = data.LastUpdateDate;
                    temp.TotalView = data.TotalView;

                    if (isCommit)
                    {
                        db.SaveChanges();

                        message = "Save data bank success";
                        result = true;
                    }

                  
                }

                res.ID = data.ID;
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

        public void CountViewSite(Guid siteID)
        {
            var siteInfo = GetSiteByID(siteID).FirstOrDefault();

            siteInfo.TotalView += 1;

            db.SaveChanges();
        }
        
        public Response Delete(Guid titikLokasiID, Guid userID)
        {
            var find = db.TitikLokasi.Where(x => x.ID == titikLokasiID).FirstOrDefault();
            Response res = new Response();

            if (find != null)
            {
                find.DeletedDate = DateTime.Now;
                find.DeletedByUserID = userID;

                db.SaveChanges();

                res.ID = titikLokasiID;
                res.Message = "Berhasil hapus data titiklokasi";
                res.Result = true;
            } else
            {
                res.ID = Guid.Empty;
                res.Message = "Gagal hapus data titiklokasi";
                res.Result = false;
            }

            return res;

        }

        public Response ChangeStatus(Guid titikLokasiID, Guid userID, int value)
        {
            var find = db.TitikLokasi.Where(x => x.ID == titikLokasiID).FirstOrDefault();
            Response res = new Response();

            if (find != null)
            {
                find.LastUpdateDate = DateTime.Now;
                find.LastUpdateByUserID = userID;
                find.Status = value;

                db.SaveChanges();

                res.ID = titikLokasiID;
                res.Message = "Berhasil hapus data titiklokasi";
                res.Result = true;
            }
            else
            {
                res.ID = Guid.Empty;
                res.Message = "Gagal hapus data titiklokasi";
                res.Result = false;
            }

            return res;

        }


    }
}
