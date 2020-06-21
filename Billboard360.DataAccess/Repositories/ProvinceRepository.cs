using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;

namespace Billboard360.DataAccess.Repositories
{
    public class ProvinceRepository
    {
        DatabaseContext db = new DatabaseContext();
        public ProvinceRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public IQueryable<Province> GetAll()
        {
            return db.Province.Where(x => x.DeletedDate == null);
        }

        public IQueryable<Province> GetAllFromTitikLokasiTable()
        {
            var res = (from x in db.TitikLokasi
                       select new Province()
                       {
                           Provinsi = x.Provinsi,
                       });

            return res;
        }

        public Response Insert(Province data)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                if (data.ID == Guid.Empty)
                {
                    data.ID = Guid.NewGuid();

                    db.Add(data);

                    db.SaveChanges();

                    message = "Save data provice success";
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

        public Response InsertList(List<Province> data)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                if (data.Count > 0)
                {
                    
                    foreach(var x in data)
                    {
                        db.Add(x);
                    }
                    db.SaveChanges();

                    message = "Sync data provice success";
                    result = true;
                }

                res.ID = Guid.NewGuid();
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

        public Response Update(Province data)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                if (data.ID != Guid.Empty)
                {

                    var find = (from ds in db.Province
                                where ds.ID == data.ID
                                select ds).FirstOrDefault();

                    find.Kode = data.Kode;
                    find.Provinsi = data.Provinsi;
                    find.LastUpdateDate = data.LastUpdateDate;
                    find.LastUpdateByUserID = data.LastUpdateByUserID;


                    db.SaveChanges();

                    message = "Update data master Provinsi success";
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

        public Response DeleteProvince(Province data)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                if (data.ID != Guid.Empty)
                {


                    var find = (from ds in db.Province
                                where ds.ID == data.ID
                                select ds).FirstOrDefault();

                    find.DeletedByUserID = data.DeletedByUserID;
                    find.DeletedDate = DateTime.Now;

                    db.SaveChanges();

                    message = "Delete data master Provinsi success";
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

    
    }
}
