using System;
using System.Collections.Generic;
using System.Text;
using Billboard360.DataAccess.Entities;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Models;
using System.Linq;


namespace Billboard360.DataAccess.Repositories
{
    public class CityRepository
    {
        DatabaseContext db = new DatabaseContext();
        public CityRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public IQueryable<City> GetCities(int kodeProv)
        {
            return db.City.Where(x => x.DeletedDate == null && x.KodeProvinsi == kodeProv);
        }

        public IQueryable<City> GetAllCities()
        {
            return db.City.Where(x => x.DeletedDate == null);
        }

        public IQueryable<City> GetAllCitiesFromTitikLokasi()
        {
            var res = (from x in db.TitikLokasi
                       select new City()
                       {
                           CityName = x.Kota,
                       });

            return res;
        }

        public IQueryable<City> GetCitiesDetail(Guid cityID)
        {
            return db.City.Where(x => x.DeletedDate == null && x.ID == cityID);
        }

        public Response Insert(City data)
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

                    message = "Simpan data Kota success";
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

        public Response InsertList(List<City> data)
        {
       
            Response res = new Response();

            try
            {
                if (data.Count > 0)
                {

                    foreach (var x in data)
                    {
                        db.Add(x);
                    }
                    db.SaveChanges();

                }

                res.Message = "Success sync city";
                res.Result = true;
                return res;

            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Result = false;

                return res;
            }
        }

        public Response Update(City data)
        {
            string message = "Failed";
            bool result = false;
            Response res = new Response();

            try
            {
                if (data.ID != Guid.Empty)
                {

                    var find = (from ds in db.City
                                where ds.ID == data.ID
                                select ds).FirstOrDefault();

                    find.Kode = data.Kode;
                    find.CityName = data.CityName;
                    find.ProvinceID = data.ProvinceID;
                    find.KodeProvinsi = data.KodeProvinsi;
                    find.LastUpdateDate = data.LastUpdateDate;
                    find.LastUpdateByUserID = data.LastUpdateByUserID;


                    db.SaveChanges();

                    message = "Update data master KOta success";
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
