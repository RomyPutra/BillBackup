using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;
using Billboard360.DataAccess.Models.Enums;

namespace Billboard360.DataAccess.Repositories
{
    public class SiteItemRepository
    {
        DatabaseContext db = new DatabaseContext();
        public SiteItemRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public Response DeleteBySiteID(Guid titikLokasiID, Guid userID, bool isCommit = true) 
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                if (titikLokasiID != Guid.Empty)
                {

                    //sampe aku ganteng yo salah cuks
                    var find = (from ds in db.TitikLokasiDetail
                                where ds.TitikLokasiID == titikLokasiID && ds.DeletedDate == null
                                select ds).ToList();

                    //find.DeletedByUserID = userID;
                    //find.DeletedDate = DateTime.Now;

                    foreach(var x in find)
                    {
                        x.DeletedByUserID = userID;
                        x.DeletedDate = DateTime.Now;


                        if(isCommit)
                        {
                            db.SaveChanges();

                            message = "Delete success";
                            result = true;
                        }
                        
                    }
                    
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

        public Response Insert(TitikLokasiDetail data, bool isCommit = true)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                //db.Add(data);
                db.Set<TitikLokasiDetail>().AddRange(data);

                if (isCommit)
                {
                    db.SaveChanges();

                    message = "Save data Image success";
                    result = true;
                }

                res.ID = data.TitikLokasiID;
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

        public Response Update(Guid siteID, List<TitikLokasiDetail> data, bool isCommit = true, ModeEnum mode = ModeEnum.Insert)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {

                if(mode == ModeEnum.Edit)
                {
                    //buat loop 
                    List<TitikLokasiDetail> listSiteItem = new List<TitikLokasiDetail>();

                    foreach(var cur in data)
                    {
                        TitikLokasiDetail item = new TitikLokasiDetail();

                        var check = db.TitikLokasiDetail.Where(x => x.ID == cur.TitikLokasiID).FirstOrDefault();

                        if(check != null)
                        {
                            check.ID = cur.ID;
                            check.Keterangan = cur.Keterangan;
                            check.KodeArahLokasi = cur.KodeArahLokasi;
                            check.LastUpdateByUserID = cur.LastUpdateByUserID;
                            check.LastUpdateDate = cur.LastUpdateDate;
                            check.CreateByUserID = cur.CreateByUserID;
                            check.CreateDate = cur.CreateDate;
                            check.DeletedByUserID = cur.DeletedByUserID;
                            check.DeletedDate = cur.DeletedDate;
                            check.ArahLokasi = cur.ArahLokasi;
                            check.Panjang = cur.Panjang;
                            check.Lebar = cur.Lebar;
                            check.TitikLokasiID = cur.TitikLokasiID;

                            listSiteItem.Add(check);
                        }
                    }
                    db.Set<TitikLokasiDetail>().AddRange(listSiteItem);

                    if (isCommit)
                    {
                        db.SaveChanges();

                        message = "Save data Success";
                        result = true;
                    }

                    
                } else
                {
                    db.Set<TitikLokasiDetail>().AddRange(data);

                    if (isCommit)
                    {
                        db.SaveChanges();

                        message = "Save data Success";
                        result = true;
                    }
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
